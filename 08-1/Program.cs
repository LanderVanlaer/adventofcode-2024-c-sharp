using System.Numerics;
using _08_1;

var antennaCollection = new AntennaCollection();

int y;
var x = 0;
using (StreamReader file = new(args[0]))
{
    y = 0;
    while (await file.ReadLineAsync() is { } line)
    {
        x = 0;
        foreach (var c in line)
        {
            if (c != '.')
                antennaCollection.AddAntenna(
                    new Antenna(new Vector2(x, y), c)
                );

            ++x;
        }

        ++y;
    }
}

antennaCollection.MaxSize = new Vector2(x - 1, y - 1);

Console.WriteLine(antennaCollection.GetAntinodes().Count());