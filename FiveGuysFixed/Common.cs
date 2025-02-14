using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Common
{
    public enum Dir
    {
        UP, DOWN, LEFT, RIGHT
    }
    public static class MovementCommandManager
    {
        // Using a Stack for key ordering.
        private static readonly Stack<Dir> activeDirections = new Stack<Dir>();

        public static void AddDirection(Dir dir)
        {
            // Avoid adding duplicate on top
            if (activeDirections.Count == 0 || activeDirections.Peek() != dir)
            {
                if (!activeDirections.Contains(dir))
                {
                    activeDirections.Push(dir);
                }
            }
        }

        public static void RemoveDirection(Dir dir)
        {
            if (activeDirections.Count > 0 && activeDirections.Peek() == dir)
            {
                activeDirections.Pop();
            }
            else
            {
                // Remove from anywhere in the stack.
                var tempStack = new Stack<Dir>();
                while (activeDirections.Count > 0)
                {
                    var d = activeDirections.Pop();
                    if (d != dir)
                    {
                        tempStack.Push(d);
                    }
                }
                while (tempStack.Count > 0)
                {
                    activeDirections.Push(tempStack.Pop());
                }
            }
        }

        public static Dir? GetCurrentDirection()
        {
            if (activeDirections.Count > 0)
            {
                return activeDirections.Peek();
            }
            return null;
        }
    }
}
