using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.Commands
{
    public class MovementCommand : ICommand
    {
        private Dir direction;
        private bool isKeyDown; // true for key down, false for key up

        public MovementCommand(Game1 game, Dir direction, bool isKeyDown)
        {
            this.direction = direction;
            this.isKeyDown = isKeyDown;
        }

        public void Execute()
        {
            if (isKeyDown)
            {
                // When key is pressed, add the direction.
                MovementCommandManager.AddDirection(direction);
            }
            else
            {
                // When key is released, remove the direction.
                MovementCommandManager.RemoveDirection(direction);
            }

            // Determine current active direction.
            Dir? currentDir = MovementCommandManager.GetCurrentDirection();

            if (currentDir.HasValue)
            {
                GameState.Player.Move(currentDir.Value);
            }
            else
            {
                // If no keys are pressed, stop movement.
                GameState.Player.Idle();
            }
        }
    }

}
