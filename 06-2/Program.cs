using System.Numerics;
using _06_2;
using Field = System.Collections.Generic.List<System.Collections.Generic.List<bool>>;

var playField = new Field();

Vector2 basePosition = default;
var baseDirection = new Vector2(0, -1);

// read the input file
using (StreamReader file = new(args[0]))
{
    var y = 0;
    while (await file.ReadLineAsync() is { } line)
    {
        playField.Add(line.Select((c, x) =>
        {
            if (c == '^') basePosition = new Vector2(x, y);

            return c == '#';
        }).ToList());

        ++y;
    }
}

if (basePosition == default) throw new InvalidOperationException("No starting position found.");

var position = basePosition;
var direction = baseDirection;

var movedField = new bool[playField.Count, playField[0].Count];

// find out what are possible locations to place an item
while (position.Y >= 0 && position.Y < playField.Count && position.X >= 0 && position.X < playField[0].Count)
{
    if (playField[(int)position.Y][(int)position.X])
    {
        position -= direction;

        direction = direction.RotateRight();
    }
    else
    {
        movedField[(int)position.Y, (int)position.X] = true;
    }

    position += direction;
}

var count = 0;
// For each of the possible locations, check whether it creates a loop or not
for (var y = 0; y < playField.Count; ++y)
for (var x = 0; x < playField[0].Count; ++x)
    if (movedField[y, x])
    {
        var validationRun = new ValidationRun(playField, new Vector2(x, y));

        if (validationRun.Run(basePosition, baseDirection)) ++count;
    }

Console.WriteLine(count);