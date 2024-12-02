StreamReader file = new(args[0]);

var totalSafe = 0;

while (await file.ReadLineAsync() is { } line)
{
    var enumerator = line.Split(' ').Select(int.Parse).GetEnumerator();

    enumerator.MoveNext();
    var prev = enumerator.Current;

    enumerator.MoveNext();
    var current = enumerator.Current;

    var downWards = current < prev;
    var isSafe = current - prev is not (0 or > 3 or < -3);

    while (enumerator.MoveNext() && isSafe)
    {
        prev = current;
        current = enumerator.Current;

        if (current - prev is 0 or > 3 or < -3
            || (downWards && prev < current)
            || (!downWards && prev > current))
            isSafe = false;
    }

    if (isSafe) ++totalSafe;
}

file.Close();

Console.WriteLine(totalSafe);