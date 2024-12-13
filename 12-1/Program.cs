using _12_1;

var c = new List<string>();

using (var file = new StreamReader(args[0]))
{
    while (await file.ReadLineAsync() is { } line) c.Add(line);
}

var cropGroupList = new List<CropGroup>();
var visited = new bool[c.Count, c[0].Length];

for (var y = 0; y < c.Count; y++)
for (var x = 0; x < c[y].Length; x++)
{
    if (visited[y, x]) continue;

    cropGroupList.Add(BreathFirst(x, y));
}

Console.WriteLine(cropGroupList.Sum(group => group.TotalCrops * group.TotalPerimeter));

return;

CropGroup BreathFirst(int x, int y)
{
    var group = new CropGroup();
    var queue = new Queue<(int x, int y)>();
    queue.Enqueue((x, y));

    visited[y, x] = true;

    while (queue.TryDequeue(out var r))
    {
        var (cX, cY) = r;

        group.TotalCrops++;
        group.TotalPerimeter += CountPerimeter(cX, cY);

        QueueNeighbors(queue, cX, cY);
    }

    return group;
}

void QueueNeighbors(Queue<(int x, int y)> queue, int x, int y)
{
    (int, int)[] sides = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    foreach (var (nX, nY) in sides)
    {
        var dX = x + nX;
        var dY = y + nY;

        if (dX < 0 || dX >= c[0].Length
                   || dY < 0 || dY >= c.Count
                   || c[dY][dX] != c[y][x]
                   || visited[dY, dX])
            continue;

        visited[dY, dX] = true;

        queue.Enqueue((dX, dY));
    }
}

int CountPerimeter(int x, int y)
{
    (int, int)[] sides = [(1, 0), (-1, 0), (0, 1), (0, -1)];

    var perimeter = 0;

    foreach (var (nX, nY) in sides)
    {
        var dX = x + nX;
        var dY = y + nY;

        if (dX < 0 || dX >= c[0].Length
                   || dY < 0 || dY >= c.Count
                   || c[dY][dX] != c[y][x])
            ++perimeter;
    }

    return perimeter;
}