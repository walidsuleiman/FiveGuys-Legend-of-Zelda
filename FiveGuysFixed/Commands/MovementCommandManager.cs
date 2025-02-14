using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public static class MovementCommandManager
    {
        public enum Dir
        {
            UP, DOWN, RIGHT, LEFT
        }
        private static readonly List<Dir> activeDirections = new List<Dir>();

        public static void AddDirection(Dir dir)
        {
            if (!activeDirections.Contains(dir))
                activeDirections.Add(dir);
        }

        public static void RemoveDirection(Dir dir)
        {
            activeDirections.Remove(dir);
        }

        public static Dir? GetCurrentDirection()
        {
            if (activeDirections.Count > 0)
            {
                return activeDirections.Last();
            }
            return null;
        }
    }
}
