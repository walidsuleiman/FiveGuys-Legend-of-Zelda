using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public class ItemSwitchCommand : ICommand
    {
        private Game1 game;
        private bool nextItem;

        public ItemSwitchCommand(Game1 game, bool nextItem)
        {
            this.game = game;
            this.nextItem = nextItem;
        }

        public void Execute()
        {
            if (game.items.Count == 0) return;

            if (nextItem)
            {
                game.activeItemIndex++;
            }
            else
            {
                game.activeItemIndex--;
            }
            if (game.activeItemIndex > game.items.Count - 1)
            {
                game.activeItemIndex = 0;
            }
            else if (game.activeItemIndex < 0)
            {
                game.activeItemIndex = game.items.Count - 1;
            }
        }
    }
}




