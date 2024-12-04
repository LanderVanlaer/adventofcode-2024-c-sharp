StreamReader file = new(args[0]);

var totalSafe = 0;

while (await file.ReadLineAsync() is { } line)
{
    var arr = line.Split(' ').Select(int.Parse).ToArray();

    for (var i = -1; i < arr.Length; i++)
        if (IsSafe(EnumerateExcept(arr, i).GetEnumerator()))
        {
            ++totalSafe;
            break;
        }
}

file.Close();

Console.WriteLine(totalSafe);
return;

bool IsSafe(IEnumerator<int> enumerator)
{
    enumerator.MoveNext();
    var prev = enumerator.Current;

    enumerator.MoveNext();
    var current = enumerator.Current;

    var downWards = current < prev;

    if (current - prev is 0 or > 3 or < -3) return false;

    while (enumerator.MoveNext())
    {
        prev = current;
        current = enumerator.Current;

        if (current - prev is 0 or > 3 or < -3
            || (downWards && prev < current)
            || (!downWards && prev > current))
            return false;
    }

    return true;
}

IEnumerable<int> EnumerateExcept(int[] source, int index)
{
    for (var i = 0; i < source.Length; i++)
    {
        if (i == index) continue;
        yield return source[i];
    }
}