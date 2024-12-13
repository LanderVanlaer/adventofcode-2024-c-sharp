var dir = new Dictionary<ulong, ulong>();

using (StreamReader file = new(args[0]))
{
    var line = await file.ReadLineAsync();

    foreach (var s in line!.Split(' ').Select(ulong.Parse))
        if (!dir.TryAdd(s, 1))
            dir[s] += 1;
}

for (var i = 0; i < 75; i++)
{
    var newDir = new Dictionary<ulong, ulong>();

    foreach (var (value, count) in dir)
        if (value == 0)
        {
            Add(1, count);
        }
        else
        {
            var log10 = (long)Math.Log10(value);

            if (log10 % 2 == 1)
            {
                var half = (ulong)Math.Pow(10, (double)(log10 + 1) / 2);
                Add(value / half, count);
                Add(value % half, count);
            }
            else
            {
                Add(value * 2024, count);
            }
        }

    dir = newDir;

    continue;

    void Add(ulong value, ulong count)
    {
        if (!newDir.TryAdd(value, count)) newDir[value] += count;
    }
}

var sum = dir.Values.Aggregate(0UL, (current, count) => current + count);

Console.WriteLine(sum);