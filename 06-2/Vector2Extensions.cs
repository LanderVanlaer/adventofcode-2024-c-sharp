using System.Numerics;

namespace _06_2;

public static class Vector2Extensions
{
    public static Vector2 RotateRight(this Vector2 vector)
    {
        return vector switch
        {
            { X: 0, Y: -1 } => new Vector2(1, 0),
            { X: 1, Y: 0 } => new Vector2(0, 1),
            { X: 0, Y: 1 } => new Vector2(-1, 0),
            { X: -1, Y: 0 } => new Vector2(0, -1),
            _ => throw new InvalidOperationException("Invalid direction."),
        };
    }
}