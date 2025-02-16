using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Common
{
    public enum WeaponType
    {
        NONE,
        WOODSWORD,
        WHITESWORD,
        BOW,
    }

    public enum ItemType
    {
        NONE,
        POTIONS,
        HEARTS,
        GEMS
    }
    public enum Dir
    {
        UP, DOWN, LEFT, RIGHT
    }
    public static class MovementCommandManager
    {
        // Using a Stack for key ordering.
        private static readonly Stack<Dir> ActiveDirections = new Stack<Dir>();

        public static void AddDirection(Dir dir)
        {
            // Avoid adding duplicate on top
            if (ActiveDirections.Count == 0 || ActiveDirections.Peek() != dir)
            {
                if (!ActiveDirections.Contains(dir))
                {
                    ActiveDirections.Push(dir);
                }
            }
        }

        public static void RemoveDirection(Dir dir)
        {
            if (ActiveDirections.Count > 0 && ActiveDirections.Peek() == dir)
            {
                ActiveDirections.Pop();
            }
            else
            {
                // Remove from anywhere in the stack.
                var tempStack = new Stack<Dir>();
                while (ActiveDirections.Count > 0)
                {
                    var d = ActiveDirections.Pop();
                    if (d != dir)
                    {
                        tempStack.Push(d);
                    }
                }
                while (tempStack.Count > 0)
                {
                    ActiveDirections.Push(tempStack.Pop());
                }
            }
        }

        public static Dir? GetCurrentDirection()
        {
            if (ActiveDirections.Count > 0)
            {
                return ActiveDirections.Peek();
            }
            return null;
        }
    }
}
