using System.CommandLine;
using Feiyue.Formatter;
using Feiyue.Formatter.CSharp;

Option<string> pathOption = new("--path") { Description = "Path to the file or directory to format", Arity = ArgumentArity.ExactlyOne };
Option<bool> checkOption = new("--check") { Description = "Check if files are formatted without modifying them" };
Option<bool> verboseOption = new("--verbose") { Description = "Show verbose output" };

RootCommand rootCommand = new("Picasso Code Formatter") { pathOption, checkOption, verboseOption };

rootCommand.SetAction(
    async (parseResult, cancellationToken) =>
    {
        var path = parseResult.GetValue(pathOption)!;
        var check = parseResult.GetValue(checkOption);
        var verbose = parseResult.GetValue(verboseOption);

        var csFiles = GetCSharpFiles(path);
        var projectFiles = GetProjectFiles(path);

        if (csFiles.Count == 0 && projectFiles.Count == 0)
        {
            Console.WriteLine("No .cs, .csproj, or .slnx files found.");
            return;
        }

        var hasChanges = false;

        foreach (var file in csFiles)
        {
            var content = await File.ReadAllTextAsync(file, cancellationToken);
            var result = await CSharpFormatter.FormatAsync(content, new CodeFormatterOptions(), cancellationToken);

            if (result.Code != content)
            {
                hasChanges = true;

                if (check)
                {
                    Console.WriteLine($"Would format: {file}");
                }
                else
                {
                    await File.WriteAllTextAsync(file, result.Code, cancellationToken);
                    if (verbose)
                        Console.WriteLine($"Formatted: {file}");
                }
            }
            else if (verbose)
            {
                Console.WriteLine($"Already formatted: {file}");
            }
        }

        foreach (var file in projectFiles)
        {
            var content = await File.ReadAllTextAsync(file, cancellationToken);
            var formattedContent = XmlProjectFormatter.FormatContent(content);

            if (formattedContent != content)
            {
                hasChanges = true;

                if (check)
                {
                    Console.WriteLine($"Would format: {file}");
                }
                else
                {
                    await File.WriteAllTextAsync(file, formattedContent, cancellationToken);
                    if (verbose)
                        Console.WriteLine($"Formatted: {file}");
                }
            }
            else if (verbose)
            {
                Console.WriteLine($"Already formatted: {file}");
            }
        }

        if (check && hasChanges)
            Environment.Exit(1);
    });

return await rootCommand.Parse(args).InvokeAsync();

static List<string> GetCSharpFiles(string path)
{
    if (File.Exists(path))
        return path.EndsWith(".cs", StringComparison.OrdinalIgnoreCase) ? [path] : [];

    if (Directory.Exists(path))
    {
        return Directory
            .EnumerateFiles(path, "*.cs", SearchOption.AllDirectories)
            .Where(f => !f.Contains("/obj/", StringComparison.Ordinal) && !f.Contains("/bin/", StringComparison.Ordinal))
            .ToList();
    }

    return [];
}

static List<string> GetProjectFiles(string path)
{
    if (File.Exists(path))
        return path.EndsWith(".csproj", StringComparison.OrdinalIgnoreCase) || path.EndsWith(".slnx", StringComparison.OrdinalIgnoreCase) ? [path] : [];

    if (Directory.Exists(path))
    {
        var csprojFiles = Directory
            .EnumerateFiles(path, "*.csproj", SearchOption.AllDirectories)
            .Where(f => !f.Contains("/obj/", StringComparison.Ordinal) && !f.Contains("/bin/", StringComparison.Ordinal));

        var slnxFiles = Directory.EnumerateFiles(path, "*.slnx", SearchOption.AllDirectories);

        return csprojFiles.Concat(slnxFiles).ToList();
    }

    return [];
}