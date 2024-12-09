using System.Diagnostics.CodeAnalysis;
using System.Numerics;

namespace _08_1;

public class AntennaCollection
{
    private readonly Dictionary<char, List<Antenna>> _antennas = new();
    public Vector2 MaxSize { get; set; }

    public void AddAntenna(Antenna antenna)
    {
        if (!_antennas.TryGetValue(antenna.Type, out var value))
        {
            value = [];
            _antennas[antenna.Type] = value;
        }

        value.Add(antenna);
    }

    [SuppressMessage("ReSharper", "CognitiveComplexity")]
    [SuppressMessage("ReSharper", "ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator")]
    public IEnumerable<Vector2> GetAntinodes()
    {
        var antinodes = new HashSet<Vector2>();

        foreach (var antennas in _antennas.Values)
        foreach (var source in antennas)
        foreach (var target in antennas.Where(a => a != source))
        {
            var diff = source.Position - target.Position;
            var antinode = source.Position + diff;

            if (antinode is { X: >= 0, Y: >= 0 } && antinode.X <= MaxSize.X && antinode.Y <= MaxSize.Y
                && antinodes.Add(antinode))
                yield return antinode;
        }
    }
}