#!/bin/bash
set -e

# Getting the absolute path to the repo root
REPO_ROOT="$(git rev-parse --show-toplevel)/backend-csharp"
PROJECT_PATH="$REPO_ROOT/tools/Formatter/Feiyue.Formatter.csproj"
BINARY_PATH="$REPO_ROOT/tools/Formatter/bin/Debug/net10.0/Feiyue.Formatter"

# Build if the binary doesn't exist
if [ ! -f "$BINARY_PATH" ]; then
    echo "Building Feiyue.Formatter..."
    dotnet build "$PROJECT_PATH" -c Debug -v q > /dev/null
fi

# Helper to run the binary
run_tool() {
    "$BINARY_PATH" "$@"
}

if [ $# -eq 0 ]; then
    # If no arguments, format modified and untracked C# files
    FILES=$(git diff --name-only --diff-filter=ACM HEAD | grep '\.cs$' || true)
    UNTRACKED=$(git ls-files --others --exclude-standard | grep '\.cs$' || true)

    ALL_FILES=$(echo -e "$FILES\n$UNTRACKED" | sort -u | grep -v '^$')

    if [ -z "$ALL_FILES" ]; then
        exit 0
    fi

    for f in $ALL_FILES; do
        if [ -f "$f" ]; then
            run_tool --path "$f"
        fi
    done
else
    # If arguments provided
    # Check if the first argument looks like a path (doesn't start with -)
    if [[ "$1" != -* ]]; then
        # Check if the tool already handles --path.
        # Actually, let's just make it simple: if arg provided, pass it as --path if checking for - fails
        run_tool --path "$@"
    else
        run_tool "$@"
    fi
fi
