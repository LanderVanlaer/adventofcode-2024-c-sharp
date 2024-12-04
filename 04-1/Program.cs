using System.Numerics;
using Field = System.Collections.Generic.List<string>;

const string searchPattern = "XMAS";

var field = new Field();
using (StreamReader file = new(args[0]))
{
    while (await file.ReadLineAsync() is { } line) field.Add(line);
}

var total = 0;

for (var y = 0; y < field.Count; y++)
for (var x = 0; x < field[y].Length; x++)
{
    if (field[y][x] != searchPattern[0]) continue;

    var position = new Vector2(x, y);

    if (IsValid(field, position, Vector2.UnitX)) total++;
    if (IsValid(field, position, -Vector2.UnitX)) total++;
    if (IsValid(field, position, Vector2.UnitY)) total++;
    if (IsValid(field, position, -Vector2.UnitY)) total++;
    if (IsValid(field, position, Vector2.One)) total++;
    if (IsValid(field, position, -Vector2.One)) total++;
    if (IsValid(field, position, new Vector2(1, -1))) total++;
    if (IsValid(field, position, new Vector2(-1, 1))) total++;
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