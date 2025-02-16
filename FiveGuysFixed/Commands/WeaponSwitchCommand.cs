using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.LinkPlayer;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;

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
                GameState.PlayerState.heldWeapon = WeaponType.WOODSWORD;
            }
            else if(weaponNumber == 2)
            {
                GameState.PlayerState.heldWeapon = WeaponType.WHITESWORD;
            }
            else if (weaponNumber == 3)
            {
                //game.activeWeaponIndex = 3;
            }
        }
    }
}
