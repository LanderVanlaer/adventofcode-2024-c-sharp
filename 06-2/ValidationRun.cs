using System.Numerics;

namespace _06_2;

using Field = List<List<bool>>;

public class ValidationRun(Field playField, Vector2 extraObstacle)
{
    private readonly Vector2[,] _referencePoints = new Vector2[playField.Count, playField[0].Count];

    public bool Run(Vector2 position, Vector2 direction)
    {
        while (position.Y >= 0 && position.Y < playField.Count && position.X >= 0 && position.X < playField[0].Count)
        {
            if (playField[(int)position.Y][(int)position.X] || extraObstacle == position)
            {
                position -= direction;

                direction = direction.RotateRight();
            }
            else if (_referencePoints[(int)position.Y, (int)position.X] == default)
            {
                _referencePoints[(int)position.Y, (int)position.X] = direction;
            }
            else if (_referencePoints[(int)position.Y, (int)position.X] == direction)
            {
                return true;
            }

            position += direction;
        }

        return false;
    }
}