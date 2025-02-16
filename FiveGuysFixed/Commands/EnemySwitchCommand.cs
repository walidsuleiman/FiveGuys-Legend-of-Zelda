using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public class EnemySwitchCommand : ICommand
    {
        private Game1 game;
        private bool nextEnemy;

        public EnemySwitchCommand(Game1 game, bool nextEnemy)
        {
            this.game = game;
            this.nextEnemy = nextEnemy;
        }

        public void Execute()
        {
            if (game.enemies.Count == 0) return;

            if (nextEnemy)
            {
                game.activeEnemyIndex++;
            }
            else
            {
                game.activeEnemyIndex--;
            }
            if (game.activeEnemyIndex > game.enemies.Count - 1)
            {
                game.activeEnemyIndex = 0;
            }
            else if (game.activeEnemyIndex < 0)
            {
                game.activeEnemyIndex = game.enemies.Count - 1;
            }
        }
    }
}
