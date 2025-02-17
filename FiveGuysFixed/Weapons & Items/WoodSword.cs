using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework.Content;

namespace FiveGuysFixed.Items
{
    public class WoodSword 
    {
        private Dir dir;
        private Rectangle sourceRect;
        private LinkSwordAnimation swordAttack;

        public WoodSword(Dir dir)
        {
            this.dir = dir;
            swordAttack = new LinkSwordAnimation(dir, WeaponType.WOODSWORD);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            swordAttack.Draw(spriteBatch, null);
        }

        public void Update(GameTime gameTime)
        {
            swordAttack.Update(gameTime);
        }

        public void LoadContent(ContentManager content)
        {
            swordAttack.LoadContent(content);
        }
    }
}
