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
        private bool isForward;

        public EnemySwitchCommand(Game1 game, bool isForward)
        {
            this.game = game;
            this.isForward = isForward;
        }

        public void Execute()
        {
            if (game.enemies.Count == 0) return;

            if (isForward)
            {
                game.ActiveEnemyIndex = (game.ActiveEnemyIndex + 1) % game.Enemies.Count;
            }
            else
            {
                game.ActiveEnemyIndex = (game.ActiveEnemyIndex - 1 + game.Enemies.Count) % game.Enemies.Count;
            }
        }
    }
}
