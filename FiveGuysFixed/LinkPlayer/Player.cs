using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {
        private Vector2 position;
        private bool isMoving;
        private LinkWalkAnimation linkSprite;
        private double timeElapsedMoving;
        private Dir dir;

        public void move(Dir newDir) 
        {
            dir = newDir;
            linkSprite.animate();
            isMoving = true;
            linkSprite.facingDirection(newDir);
        }
        public void idle() 
        {
            isMoving = false;
            linkSprite.idle();
        }

        public void attack() { }
        public void switchItem() { }
        public void Draw(SpriteBatch _spriteBatch) 
        {
            linkSprite.Draw(_spriteBatch, position);
        }
        public void Update(GameTime gt)
        {
            Vector2 newPos = position;
            if (isMoving)
            {
                if (dir == Dir.UP)
                {
                    newPos.Y -= 5;
                }
                else if (dir == Dir.DOWN)
                {
                    newPos.Y += 5;
                }
                else if (dir == Dir.RIGHT)
                {
                    newPos.X += 5;
                }
                else if (dir == Dir.LEFT)
                {
                    newPos.X -= 5;
                }
            }
            position = newPos;
            linkSprite.Update(gt);
        }
        public void LoadContent(ContentManager content)
        {
            linkSprite.LoadContent(content);
            linkSprite.facingDirection(dir);
        }
        public void Reset() 
        { 
            position = new Vector2(100, 100);
            isMoving = false;
        }
        public void takeDamage(int damage) { }
        public void gainHealth(int health) { }

        public Player(Vector2 setPos)
        {
            linkSprite = new LinkWalkAnimation();
            position = setPos;
            isMoving = false;
            dir = Dir.DOWN;
        }

    }
}
