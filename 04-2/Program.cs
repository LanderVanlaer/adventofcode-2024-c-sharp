using System.Numerics;
using Field = System.Collections.Generic.List<string>;

const string searchPattern = "MAS";

var field = new Field();
using (StreamReader file = new(args[0]))
{
    while (await file.ReadLineAsync() is { } line) field.Add(line);
}

var total = 0;

var leftTop = new Vector2(-1, -1);
var rightBottom = new Vector2(1, 1);
var leftBottom = new Vector2(-1, 1);
var rightTop = new Vector2(1, -1);

var maxY = field.Count - 1;
var maxX = field[0].Length - 1;
for (var y = 1; y < maxY; y++)
for (var x = 1; x < maxX; x++)
{
    if (field[y][x] != searchPattern[1]) continue;

    var position = new Vector2(x, y);

    if ((IsValid(field, position + leftTop, rightBottom)
         || IsValid(field, position + rightBottom, leftTop))
        && (IsValid(field, position + leftBottom, rightTop)
            || IsValid(field, position + rightTop, leftBottom))
       )
        total++;
}

Console.WriteLine(total);

return;

bool IsValid(Field f, Vector2 position, Vector2 direction, int searchIndex = 0)
{
    if (position.X < 0
        || position.Y < 0
        || position.X >= f[0].Length
        || position.Y >= f.Count) return false;

    return f[(int)position.Y][(int)position.X] == searchPattern[searchIndex]
           && (searchIndex == searchPattern.Length - 1
               || IsValid(f, position + direction, direction, searchIndex + 1));
}