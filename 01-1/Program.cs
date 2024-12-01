StreamReader file = new(args[0]);

var comparer = Comparer<int>.Create((a, b) => a == b ? -1 : a - b);

SortedSet<int> leftList = new(comparer);
SortedSet<int> rightList = new(comparer);

while (await file.ReadLineAsync() is { } line)
{
    leftList.Add(int.Parse(line[..5]));
    rightList.Add(int.Parse(line[8..]));
}

file.Close();

var total = leftList.Zip(rightList).Aggregate(0, (acc, pair) => acc + Math.Abs(pair.First - pair.Second));

Console.WriteLine(total);