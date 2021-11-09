using System;

namespace DungeonCrawl
{
    public enum Direction : byte
    {
        Up,
        Down,
        Left,
        Right,
        None
    }

    public static class Utilities
    {
        public static Random Random = new Random();
        public static (int x, int y) ToVector(this Direction dir)
        {
            switch (dir)
            {
                case Direction.Up:
                    return (0, 1);
                case Direction.Down:
                    return (0, -1);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Right:
                    return (1, 0);
                default:
                    throw new ArgumentOutOfRangeException(nameof(dir), dir, null);
            }
        }
    }
}
