using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

StreamReader file = new(args[0]);

var total = 0;
var regex = MyRegex();

while (await file.ReadLineAsync() is { } line)
{
    var matches = regex.Matches(line);
    if (matches.Count == 0) continue;

    foreach (Match match in matches)
    {
        var a = int.Parse(match.Groups[1].Value);
        var b = int.Parse(match.Groups[2].Value);
        total += a * b;
    }
}

file.Close();

Console.WriteLine(total);

[SuppressMessage("ReSharper", "UnusedType.Global")]
internal partial class Program
{
    [GeneratedRegex(@"mul\((\d+),(\d+)\)")]
    private static partial Regex MyRegex();
}