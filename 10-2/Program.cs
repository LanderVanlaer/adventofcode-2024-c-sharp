using System.Numerics;
using _10_1;

Vector2[] positions =
[
    new(-1, 0),
    new(1, 0),
    new(0, -1),
    new(0, 1),
];

List<short[]> matrix = [];

using (StreamReader file = new(args[0]))
{
    while (await file.ReadLineAsync() is { } line) matrix.Add(line.Select(c => (short)(c - '0')).ToArray());
}

var connectsToHeads = new Dictionary<Vector2, List<Vector2>>();
var queue = new Queue<QueueBlock>();

for (var y = 0; y < matrix.Count; y++)
for (var x = 0; x < matrix[y].Length; x++)
    if (matrix[y][x] == 9)
    {
        var from = new Vector2(x, y);
        connectsToHeads[from] = [from];

        foreach (var to in GetPositionsOneLower(from, 9)) queue.Enqueue(new QueueBlock(from, to));
    }

while (queue.TryDequeue(out var block))
{
    if (!connectsToHeads.TryGetValue(block.To, out var value))
    {
        value = [];
        connectsToHeads[block.To] = value;

        var currentValue = matrix[(int)block.To.Y][(int)block.To.X];

        foreach (var to in GetPositionsOneLower(block.To, currentValue))
            queue.Enqueue(new QueueBlock(block.To, to));
    }

    value.AddRange(connectsToHeads[block.From]);
}

var sum = 0;

// For every 0, print the number of heads that can reach it
for (var y = 0; y < matrix.Count; y++)
for (var x = 0; x < matrix[y].Length; x++)
    if (matrix[y][x] == 0)
        sum += connectsToHeads[new Vector2(x, y)].Count;

Console.WriteLine(sum);

return;

IEnumerable<Vector2> GetPositionsOneLower(Vector2 currentPosition, int currentValue)
{
    foreach (var position in positions)
    {
        var newPosition = currentPosition + position;

        if (newPosition.X < 0 || newPosition.X >= matrix[0].Length ||
            newPosition.Y < 0 || newPosition.Y >= matrix.Count)
            continue;

        if (matrix[(int)newPosition.Y][(int)newPosition.X] == currentValue - 1) yield return newPosition;
    }
}