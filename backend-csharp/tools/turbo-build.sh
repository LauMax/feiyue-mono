#!/bin/bash
#
# turbo-build.sh - Ultra-fast incremental build for Feiyue
#
# This script:
# 1. Detects which projects have changed files
# 2. Builds ONLY those projects (no dependents) in parallel
# 3. Copies outputs to all dependent projects' bin/ folders
#
# WARNING: This assumes APIs are stable. If you change a public API,
# dependents may fail at runtime. Use `dotnet build` for safety.
#
# Usage:
#   ./tools/turbo-build.sh              # Build changed projects
#   ./tools/turbo-build.sh --all        # Rebuild everything
#   ./tools/turbo-build.sh --project X  # Build specific project
#   ./tools/turbo-build.sh --help       # Show help

set -euo pipefail

START_TIME=$(date +%s.%N 2>/dev/null || date +%s)

SCRIPT_DIR="$(cd "$(dirname "${BASH_SOURCE[0]}")" && pwd)"
REPO_ROOT="$(cd "$SCRIPT_DIR/.." && pwd)"
GRAPH_DIR="$SCRIPT_DIR/build-graph"
GRAPH_FILE="$GRAPH_DIR/dependency-graph.txt"
REVERSE_GRAPH_FILE="$GRAPH_DIR/reverse-graph.txt"
PROJECT_MAP_FILE="$GRAPH_DIR/project-map.txt"

# Detect target framework from Directory.Build.props
TARGET_FRAMEWORK=$(grep -oE '<TargetFramework>[^<]+' "$REPO_ROOT/Directory.Build.props" 2>/dev/null | sed 's/<TargetFramework>//' | head -1)
TARGET_FRAMEWORK="${TARGET_FRAMEWORK:-net10.0}"

RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
BLUE='\033[0;34m'
NC='\033[0m'

log_info() { echo -e "${BLUE}ℹ️  $1${NC}"; }
log_success() { echo -e "${GREEN}✅ $1${NC}"; }
log_warn() { echo -e "${YELLOW}⚠️  $1${NC}"; }
log_error() { echo -e "${RED}❌ $1${NC}"; }

show_help() {
    cat << 'EOF'
turbo-build.sh - Ultra-fast incremental build for Feiyue

USAGE:
    ./tools/turbo-build.sh [OPTIONS]

OPTIONS:
    --all              Rebuild all projects
    --changed          Show changed projects without building
    --project NAME     Build specific project(s) by name (can repeat)
    --full             Do a full dotnet build (use after major changes)
    --regenerate-graph Regenerate the dependency graph (after adding projects)
    --help             Show this help

EXAMPLES:
    ./tools/turbo-build.sh                          # Build changed projects
    ./tools/turbo-build.sh --project Service.Api    # Build Service.Api only
    ./tools/turbo-build.sh --all                    # Rebuild everything

WARNING:
    This script does NOT rebuild dependents when a dependency changes.
    It only rebuilds the changed project and copies the DLL to dependents.
    Use regular `dotnet build` if you've changed public APIs.
EOF
}

# Verify static graph files exist and are up-to-date
ensure_graph() {
    local needs_regen=false
    local cache_marker="$REPO_ROOT/.turbo-build-cache"
    local cache_commit="$REPO_ROOT/.turbo-build-commit"
    
    # Check if graph files exist
    if [[ ! -f "$GRAPH_FILE" || ! -f "$PROJECT_MAP_FILE" ]]; then
        needs_regen=true
    elif [[ ! -f "$cache_commit" ]]; then
        needs_regen=true
    else
        local last_commit
        last_commit=$(cat "$cache_commit" 2>/dev/null || echo "")
        local current_commit
        current_commit=$(git rev-parse HEAD 2>/dev/null || echo "")
        
        # Check if any csproj changed between last cached commit and now
        if [[ -n "$last_commit" && -n "$current_commit" && "$last_commit" != "$current_commit" ]]; then
            local changed_csproj
            changed_csproj=$(git diff --name-only "$last_commit" HEAD -- '*.csproj' 2>/dev/null | head -1 || echo "")
            if [[ -n "$changed_csproj" ]]; then
                needs_regen=true
            fi
        fi
        
        # Also check uncommitted changes
        if [[ "$needs_regen" != "true" ]]; then
            local uncommitted_csproj
            uncommitted_csproj=$(git diff --name-only HEAD -- '*.csproj' 2>/dev/null | head -1 || echo "")
            if [[ -n "$uncommitted_csproj" ]]; then
                needs_regen=true
            fi
        fi
    fi
    
    if [[ "$needs_regen" == "true" ]]; then
        regenerate_graph
    fi
    
    # Update cache markers
    if command -v git &> /dev/null && git rev-parse HEAD &> /dev/null; then
        git rev-parse HEAD > "$cache_commit" 2>/dev/null || true
    fi
    touch "$cache_marker"
}

# Regenerate the dependency graph (run when project references change)
regenerate_graph() {
    log_info "Regenerating dependency graph..."
    
    mkdir -p "$GRAPH_DIR"
    
    # Clear old files
    > "$GRAPH_FILE"
    > "$REVERSE_GRAPH_FILE"
    > "$PROJECT_MAP_FILE"
    
    # Find all projects and extract dependencies
    while IFS= read -r csproj; do
        local project_name
        project_name=$(basename "$csproj" .csproj)
        local project_dir
        project_dir=$(dirname "$csproj")
        
        # Store project -> directory mapping (relative paths)
        local rel_dir="${project_dir#$REPO_ROOT/}"
        local rel_csproj="${csproj#$REPO_ROOT/}"
        echo "$project_name|$rel_dir|$rel_csproj" >> "$PROJECT_MAP_FILE"
        
        # Extract ProjectReference dependencies
        local deps
        deps=$(grep -oE '<ProjectReference Include="[^"]+"' "$csproj" 2>/dev/null | \
               sed 's/<ProjectReference Include="//;s/"//' | \
               xargs -I{} basename {} .csproj 2>/dev/null || true)
        
        for dep in $deps; do
            # Forward graph: project -> its dependencies
            echo "$project_name|$dep" >> "$GRAPH_FILE"
            # Reverse graph: dependency -> projects that depend on it
            echo "$dep|$project_name" >> "$REVERSE_GRAPH_FILE"
        done
    done < <(find "$REPO_ROOT" -name "*.csproj" -not -path "*/obj/*" -not -path "*/bin/*")
    
    # Sort for faster lookups
    LC_ALL=C sort "$GRAPH_FILE" -o "$GRAPH_FILE" 2>/dev/null || sort "$GRAPH_FILE" -o "$GRAPH_FILE"
    LC_ALL=C sort "$REVERSE_GRAPH_FILE" -o "$REVERSE_GRAPH_FILE" 2>/dev/null || sort "$REVERSE_GRAPH_FILE" -o "$REVERSE_GRAPH_FILE"
    LC_ALL=C sort "$PROJECT_MAP_FILE" -o "$PROJECT_MAP_FILE" 2>/dev/null || sort "$PROJECT_MAP_FILE" -o "$PROJECT_MAP_FILE"
    
    local project_count
    project_count=$(wc -l < "$PROJECT_MAP_FILE" | tr -d ' ')
    log_success "Graph regenerated: $project_count projects"
    log_info "Commit the updated files in tools/build-graph/"
}

# Get all projects that depend on a given project (direct dependents only)
get_dependents() {
    local project="$1"
    grep "^${project}|" "$REVERSE_GRAPH_FILE" 2>/dev/null | cut -d'|' -f2 || true
}

# Get all projects that a given project depends on
get_dependencies() {
    local project="$1"
    grep "^${project}|" "$GRAPH_FILE" 2>/dev/null | cut -d'|' -f2 || true
}

# Get project directory from name (returns absolute path)
get_project_dir() {
    local project="$1"
    local rel_dir
    rel_dir=$(grep "^${project}|" "$PROJECT_MAP_FILE" 2>/dev/null | cut -d'|' -f2 | head -1 || true)
    if [[ -n "$rel_dir" ]]; then
        echo "$REPO_ROOT/$rel_dir"
    fi
}

# Get project csproj path from name (returns absolute path)
get_project_path() {
    local project="$1"
    local rel_path
    rel_path=$(grep "^${project}|" "$PROJECT_MAP_FILE" 2>/dev/null | cut -d'|' -f3 | head -1 || true)
    if [[ -n "$rel_path" ]]; then
        echo "$REPO_ROOT/$rel_path"
    fi
}

# Get changed projects based on git status
get_changed_projects() {
    # Get uncommitted changes
    local changed_files
    changed_files=$(cd "$REPO_ROOT" && (git diff --name-only HEAD 2>/dev/null; git diff --name-only --cached HEAD 2>/dev/null) || true)
    
    local seen=""
    
    while IFS= read -r file; do
        [[ -z "$file" ]] && continue
        [[ ! "$file" =~ \.(cs|csproj|props|targets|liquid|json)$ ]] && continue
        
        # Find containing project
        local dir="$REPO_ROOT/$(dirname "$file")"
        while [[ "$dir" != "$REPO_ROOT" && "$dir" != "/" ]]; do
            local csproj
            csproj=$(ls "$dir"/*.csproj 2>/dev/null | head -1 || true)
            if [[ -n "$csproj" ]]; then
                local project_name
                project_name=$(basename "$csproj" .csproj)
                # Deduplicate inline
                if [[ ! "$seen" =~ "|${project_name}|" ]]; then
                    seen="$seen|${project_name}|"
                    echo "$project_name"
                fi
                break
            fi
            dir=$(dirname "$dir")
        done
    done <<< "$changed_files"
}

# Build a single project without dependencies
build_project() {
    local project="$1"
    local csproj
    csproj=$(get_project_path "$project")
    
    if [[ -z "$csproj" || ! -f "$csproj" ]]; then
        log_error "Project not found: $project"
        return 1
    fi
    
    # Use msbuild directly to just compile, skipping project reference builds
    # Disable analyzers, doc gen, and ref assemblies for max speed
    dotnet msbuild "$csproj" \
        -t:Build \
        -p:Configuration=Debug \
        -p:BuildProjectReferences=false \
        -p:RunAnalyzers=false \
        -p:GenerateDocumentationFile=false \
        -p:ProduceReferenceAssembly=false \
        -v:m \
        -nologo
}

# Check if a project's DLL exists
project_dll_exists() {
    local project="$1"
    local project_dir
    project_dir=$(get_project_dir "$project")
    [[ -n "$project_dir" && -f "$project_dir/bin/Debug/${TARGET_FRAMEWORK}/${project}.dll" ]]
}

# Get direct missing dependencies for a project (non-recursive, fast)
get_direct_missing_deps() {
    local project="$1"
    local deps
    deps=$(get_dependencies "$project")
    
    for dep in $deps; do
        if ! project_dll_exists "$dep"; then
            echo "$dep"
        fi
    done
}

# Copy a project's output to all its dependents
copy_to_dependents() {
    local project="$1"
    local project_dir
    project_dir=$(get_project_dir "$project")
    
    if [[ -z "$project_dir" ]]; then
        return 0
    fi
    
    local src_dll="$project_dir/bin/Debug/${TARGET_FRAMEWORK}/${project}.dll"
    local src_pdb="$project_dir/bin/Debug/${TARGET_FRAMEWORK}/${project}.pdb"
    
    if [[ ! -f "$src_dll" ]]; then
        return 0
    fi
    
    # Get all dependents
    local dependents
    dependents=$(get_dependents "$project")
    
    for dep in $dependents; do
        local dep_dir
        dep_dir=$(get_project_dir "$dep")
        if [[ -n "$dep_dir" ]]; then
            local target_dir="$dep_dir/bin/Debug/${TARGET_FRAMEWORK}"
            mkdir -p "$target_dir"
            cp -f "$src_dll" "$target_dir/" 2>/dev/null || true
            [[ -f "$src_pdb" ]] && cp -f "$src_pdb" "$target_dir/" 2>/dev/null || true
        fi
    done
}

# Build projects in parallel respecting dependencies between changed projects
build_parallel() {
    local projects=("$@")
    local max_parallel=8
    
    if [[ ${#projects[@]} -eq 0 ]]; then
        log_success "Nothing to build"
        return 0
    fi
    
    log_info "Building ${#projects[@]} project(s)..."
    
    # Check for missing direct dependencies
    local all_missing=()
    local seen=""
    for project in "${projects[@]}"; do
        while IFS= read -r dep; do
            [[ -z "$dep" ]] && continue
            if [[ ! "$seen" == *"|${dep}|"* ]]; then
                seen="$seen|${dep}|"
                all_missing+=("$dep")
            fi
        done < <(get_direct_missing_deps "$project")
    done
    
    if [[ ${#all_missing[@]} -gt 0 ]]; then
        log_warn "Missing ${#all_missing[@]} dependency DLL(s). Running full build first..."
        echo ""
        
        cd "$REPO_ROOT"
        if dotnet build -p:RunAnalyzers=false -v:m --nologo; then
            log_success "Full build complete"
            echo ""
        else
            log_error "Full build failed"
            return 1
        fi
    fi
    
    # Now build target projects in parallel
    local pids=()
    local running=0
    local failed=0
    
    for project in "${projects[@]}"; do
        # Wait if we've hit max parallel
        while [[ $running -ge $max_parallel ]]; do
            for i in "${!pids[@]}"; do
                if ! kill -0 "${pids[$i]}" 2>/dev/null; then
                    wait "${pids[$i]}" || ((failed++))
                    unset 'pids[i]'
                    ((running--))
                fi
            done
            sleep 0.1
        done
        
        # Start build in background
        (
            if build_project "$project"; then
                echo -e "${GREEN}✓${NC} $project"
                copy_to_dependents "$project"
            else
                echo -e "${RED}✗${NC} $project"
                exit 1
            fi
        ) &
        pids+=($!)
        ((running++))
    done
    
    # Wait for remaining
    for pid in "${pids[@]}"; do
        wait "$pid" || ((failed++))
    done
    
    if [[ $failed -gt 0 ]]; then
        log_error "$failed project(s) failed to build"
        return 1
    fi
    
    log_success "Build complete!"
}

# Main
main() {
    local MODE="build"
    local EXPLICIT_PROJECTS=()
    
    while [[ $# -gt 0 ]]; do
        case "$1" in
            --all)
                MODE="all"
                shift
                ;;
            --regenerate-graph)
                MODE="regenerate"
                shift
                ;;
            --changed)
                MODE="changed"
                shift
                ;;
            --project)
                MODE="explicit"
                EXPLICIT_PROJECTS+=("$2")
                shift 2
                ;;
            --full)
                MODE="full"
                shift
                ;;
            --help|-h)
                show_help
                exit 0
                ;;
            *)
                log_error "Unknown option: $1"
                show_help
                exit 1
                ;;
        esac
    done
    
    echo ""
    echo "⚡ Feiyue Turbo Build"
    echo "━━━━━━━━━━━━━━━━━━━━━━"
    echo ""
    
    case "$MODE" in
        regenerate)
            regenerate_graph
            ;;
        changed)
            ensure_graph
            log_info "Changed projects:"
            get_changed_projects | sed 's/^/  /'
            ;;
        all)
            ensure_graph
            log_info "Rebuilding all projects..."
            local all_projects=()
            while IFS='|' read -r name dir path; do
                all_projects+=("$name")
            done < "$PROJECT_MAP_FILE"
            build_parallel "${all_projects[@]}"
            ;;
        full)
            log_info "Running full dotnet build..."
            cd "$REPO_ROOT"
            dotnet build -v:m
            ;;
        build)
            ensure_graph
            local changed=()
            while IFS= read -r p; do
                [[ -n "$p" ]] && changed+=("$p")
            done < <(get_changed_projects)
            
            if [[ ${#changed[@]} -eq 0 ]]; then
                log_success "No changes detected"
                exit 0
            fi
            
            log_info "Changed projects:"
            printf '  %s\n' "${changed[@]}"
            echo ""
            
            build_parallel "${changed[@]}"
            ;;
        explicit)
            ensure_graph
            log_info "Building specified projects:"
            printf '  %s\n' "${EXPLICIT_PROJECTS[@]}"
            echo ""
            
            build_parallel "${EXPLICIT_PROJECTS[@]}"
            ;;
    esac
    
    # Print elapsed time
    local end_time=$(date +%s.%N 2>/dev/null || date +%s)
    local elapsed
    if command -v bc &> /dev/null; then
        elapsed=$(echo "$end_time - $START_TIME" | bc 2>/dev/null || echo "$((${end_time%.*} - ${START_TIME%.*}))")
        printf "\n⏱️  Total time: %.1fs\n" "$elapsed" 2>/dev/null || echo "⏱️  Total time: ${elapsed}s"
    else
        elapsed=$((${end_time%.*} - ${START_TIME%.*}))
        echo ""
        echo "⏱️  Total time: ${elapsed}s"
    fi
}

main "$@"
