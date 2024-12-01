StreamReader file = new(args[0]);

List<int> leftList = [];
Dictionary<int, short> rightMap = new();

while (await file.ReadLineAsync() is { } line)
{
    var l = int.Parse(line[..5]);
    var r = int.Parse(line[8..]);

    leftList.Add(l);
    rightMap[r] = (short)(rightMap.TryGetValue(r, out var count) ? count + 1 : 1);
}

file.Close();

var total = leftList.Aggregate(0, (acc, l) => rightMap.TryGetValue(l, out var count) ? acc + l * count : acc);

Console.WriteLine(total);