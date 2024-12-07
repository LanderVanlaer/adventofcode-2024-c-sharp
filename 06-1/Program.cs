using System.Numerics;
using Field = System.Collections.Generic.List<System.Collections.Generic.List<bool>>;

var playField = new Field();

Vector2 position = default;

using (StreamReader file = new(args[0]))
{
    var y = 0;
    while (await file.ReadLineAsync() is { } line)
    {
        playField.Add(line.Select((c, x) =>
        {
            if (c == '^') position = new Vector2(x, y);

            return c == '#';
        }).ToList());

        ++y;
    }
}


if (position == default) throw new InvalidOperationException("No starting position found.");

var count = 0;

var direction = new Vector2(0, -1);

var countingField = new int[playField.Count, playField[0].Count];

while (position.Y >= 0 && position.Y < playField.Count && position.X >= 0 && position.X < playField[0].Count)
{
    if (playField[(int)position.Y][(int)position.X])
    {
        position -= direction;

        direction = direction switch
        {
            { X: 0, Y: -1 } => new Vector2(1, 0),
            { X: 1, Y: 0 } => new Vector2(0, 1),
            { X: 0, Y: 1 } => new Vector2(-1, 0),
            { X: -1, Y: 0 } => new Vector2(0, -1),
            _ => throw new InvalidOperationException("Invalid direction."),
        };
    }
    else if (++countingField[(int)position.Y, (int)position.X] == 1)
    {
        ++count;
    }

    position += direction;
}

Console.WriteLine(count);