using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.LinkPlayer
{
    public interface IPlayer
    {
        public void moveUp();
        public void moveDown();
        public void moveLeft();
        public void moveRight();
        public void attack();
        public void switchItem();
        public void Draw(SpriteBatch _spriteBatch);
        public void Update(GameTime gt);
        public void LoadContent(ContentManager content);
        public void Reset();
        public void takeDamage(int damage);
        public void gainHealth(int health);
    }
}
