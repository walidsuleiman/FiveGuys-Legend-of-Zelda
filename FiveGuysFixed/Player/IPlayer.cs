using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Player
{
    public interface IPlayer
    {
        void linkMoveUp(LinkWalkAnimation linkSprite);
        void linkMoveDown(LinkWalkAnimation linkSprite);
        void linkMoveLeft(LinkWalkAnimation linkSprite);
        void linkMoveRight(LinkWalkAnimation linkSprite);
        void linkAttack();
        void linkSwitchItem();
        void linkDamage(int damage);
    }
}
