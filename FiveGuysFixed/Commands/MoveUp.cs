using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public class MoveUp : ICommand
    {
        private Game1 game;

        public MoveUp(Game1 game)
        {
            this.game = game;
        }
        public void Execute()
        {
            //game.link.MoveUp();
        }
    }
}
