using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

StreamReader file = new(args[0]);

var total = 0;
var regex = MyRegex();
var dont = false;

while (await file.ReadLineAsync() is { } line)
{
    var matches = regex.Matches(line);
    if (matches.Count == 0) continue;

    foreach (Match match in matches)
        if (match.Groups[4].Success)
        {
            dont = true;
        }
        else if (match.Groups[3].Success)
        {
            dont = false;
        }
        else if (!dont)
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
    [GeneratedRegex(@"mul\((\d+),(\d+)\)|(do\(\))|(don't\(\))")]
    private static partial Regex MyRegex();
}