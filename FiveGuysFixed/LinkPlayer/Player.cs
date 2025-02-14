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
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {
        private Vector2 position;
        private Vector2 movement;
        private bool isMoving;
        private LinkWalkAnimation linkSprite;
        private double timeElapsedMoving;

        private enum Dir 
        {
            UP, DOWN, RIGHT, LEFT
        }

        private Dir dir;

        public void moveUp()
        {
            //movement = new Vector2(0, -1);
            dir = Dir.UP;
            isMoving = true;
            linkSprite.facingUp();
        }
        public void moveDown() 
        {
            //movement = new Vector2(0, 1);
            dir = Dir.DOWN;
            isMoving = true;
            linkSprite.facingDown();
        }
        public void moveLeft() 
        {
            //movement = new Vector2(-1, 0);
            dir = Dir.LEFT;
            isMoving = true;
            linkSprite.facingLeft();
        }
        public void moveRight() 
        {
            //movement = new Vector2(1, 0);
            dir = Dir.RIGHT;
            isMoving = true;
            linkSprite.facingRight();
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
                //position += movement;
                //isMoving = false;
                timeElapsedMoving += gt.ElapsedGameTime.TotalSeconds;
                if (timeElapsedMoving >= .00001)
                {
                    isMoving = !isMoving;
                    timeElapsedMoving = 0;
                }
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
            linkSprite.facingLeft();
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
            movement = new Vector2(0, 0);
            isMoving = false;
        }

    }
}
