using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed.Commands
{
    public class WeaponSwitchCommand : ICommand
    {
        private Game1 game;
        private int weaponNumber;

        public WeaponSwitchCommand(Game1 game, int weaponNumber)
        {
            this.game = game;
            this.weaponNumber = weaponNumber;
        }
        public void Execute()
        {
            if (game.weapons.Count == 0) return;

            if (weaponNumber == 1)
            {
                game.activeWeaponIndex = 1;
            }
            else if(weaponNumber == 2)
            {
                game.activeWeaponIndex = 2;
            }
            else if (weaponNumber == 3)
            {
                game.activeWeaponIndex = 3;
            }


            if (game.activeWeaponIndex > game.weapons.Count - 1)
            {
                game.activeWeaponIndex = 0;
            }
            else if (game.activeWeaponIndex < 0)
            {
                game.activeWeaponIndex = game.weapons.Count - 1;
            }
        }
    }
}
