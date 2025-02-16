using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public class BlockSwitchCommand : ICommand
    {
        private Game1 game;
        private bool nextBlock;

        public BlockSwitchCommand(Game1 game, bool nextBlock)
        {
            this.game = game;
            this.nextBlock = nextBlock;
        }
        public void Execute()
        {
            if (game.blocks.Count == 0) return;

            if (nextBlock)
            {
                game.activeBlockIndex++;
            }
            else
            {
                game.activeBlockIndex--;
            }
            if (game.activeBlockIndex > game.blocks.Count - 1)
            {
                game.activeBlockIndex = 0;
            }
            else if (game.activeBlockIndex < 0)
            {
                game.activeBlockIndex = game.blocks.Count - 1;
            }
        }


    }
    }

