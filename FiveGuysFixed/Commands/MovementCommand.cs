using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Common;

namespace FiveGuysFixed.Commands
{
    public class MovementCommand : ICommand
    {
        private Game1 game;
        private Dir direction;
        private bool isKeyDown; // true for key down, false for key up

        public MovementCommand(Game1 game, Dir direction, bool isKeyDown)
        {
            this.game = game;
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
                game.Player.move(currentDir.Value);
            }
            else
            {
                // If no keys are pressed, stop movement.
                game.Player.idle();
            }
        }
    }

}
