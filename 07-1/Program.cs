using _07_1;

var equations = new List<Equation>();

using (StreamReader file = new(args[0]))
{
    while (await file.ReadLineAsync() is { } line)
    {
        var indexOfColon = line.IndexOf(':');
        var result = long.Parse(line[..indexOfColon]);
        var numbers = line[(indexOfColon + 2)..]
            .Split(' ')
            .Select(long.Parse)
            .ToArray();

        equations.Add(new Equation(result, numbers));
    }
}

var sum = equations.Where(equation => equation.IsValidEquation()).Sum(equation => equation.Result);

Console.WriteLine(sum);