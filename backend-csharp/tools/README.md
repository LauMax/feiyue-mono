# 代码格式化工具

基于 Roslyn 的自定义 C# 代码格式化工具，从 Picasso 项目移植。

## 快速使用

### 格式化修改的文件（推荐）
```bash
./tools/format-csharp.sh
```
自动格式化所有 Git 修改和未跟踪的 `.cs` 文件。

### 格式化特定文件或目录
```bash
# 单个文件
./tools/format-csharp.sh --path src/Service.Api/Program.cs

# 整个目录
./tools/format-csharp.sh --path src/

# 详细输出
./tools/format-csharp.sh --path src/ --verbose

# 检查模式（不修改文件）
./tools/format-csharp.sh --path src/ --check
```

## 功能特性

- ✅ **C# 代码格式化**：基于 Roslyn 语法树，支持最新 C# Preview 特性
- ✅ **项目文件格式化**：自动排序和格式化 `.csproj` 和 `.slnx` 文件
- ✅ **智能文件发现**：自动排除 `bin/` 和 `obj/` 目录
- ✅ **快速增量**：首次构建后，后续运行非常快

## 工作原理

1. 首次运行时自动构建 `Feiyue.Formatter` 工具
2. 后续运行直接使用已构建的二进制（~1-2秒处理整个项目）
3. 使用 Roslyn 解析代码并重新格式化

## 集成到开发流程

建议在提交代码前运行：
```bash
./tools/format-csharp.sh && git add -u && git commit -m "your message"
```
