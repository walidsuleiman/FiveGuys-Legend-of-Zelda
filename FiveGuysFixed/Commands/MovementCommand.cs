//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static FiveGuysFixed.Commands.MovementCommandManager;

//namespace FiveGuysFixed.Commands
//{
//    public class MovementCommand : ICommand
//    {
//        private Game1 game;
//        private Dir direction;
//        private bool isKeyDown; // true for key down, false for key up

//        public MovementCommand(Game1 game, Dir direction, bool isKeyDown)
//        {
//            this.game = game;
//            this.direction = direction;
//            this.isKeyDown = isKeyDown;
//        }

//        public void Execute()
//        {
//            if (isKeyDown)
//            {
//                // When key is pressed, add the direction
//                MovementCommandManager.AddDirection(direction);
//            }
//            else
//            {
//                // When key is released, remove the direction
//                MovementCommandManager.RemoveDirection(direction);
//            }

//            // Determine the current direction to move based on the active keys
//            Dir? currentDir = MovementCommandManager.GetCurrentDirection();

//            if (currentDir.HasValue)
//            {
//                switch (currentDir.Value)
//                {
//                    case Dir.UP:
//                        game.Player.moveUp();
//                        break;
//                    case Dir.DOWN:
//                        game.Player.moveDown();
//                        break;
//                    case Dir.LEFT:
//                        game.Player.moveLeft();
//                        break;
//                    case Dir.RIGHT:
//                        game.Player.moveRight();
//                        break;
//                }
//            }
//            else
//            {
//                // Optionally, stop movement if no keys are held.
//                // e.g., game.Player.Stop(); if such a method exists.
//            }
//        }
//    }

//}
