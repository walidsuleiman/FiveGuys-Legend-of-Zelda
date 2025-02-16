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
using FiveGuysFixed.GameStates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {
        private LinkWalkAnimation linkSprite;
        private int health;

        public Player()
        {
            linkSprite = new LinkWalkAnimation();
            health = 100;
        }
        public void move(Dir newDir) 
        {
            GameState.PlayerState.direction = newDir;
            linkSprite.animate();
            GameState.PlayerState.isMoving = true;
            linkSprite.facingDirection(newDir);
        }
        public void idle() 
        {
            GameState.PlayerState.isMoving = false;
            linkSprite.idle();
        }

        public void attack() { }
        public void switchItem() { }
        public void Draw(SpriteBatch _spriteBatch) 
        {
            linkSprite.Draw(_spriteBatch, null);
        }
        public void Update(GameTime gt)
        {
            Vector2 newPos = GameState.PlayerState.position;
            if (GameState.PlayerState.isMoving)
            {
                if (GameState.PlayerState.direction == Dir.UP)
                {
                    newPos.Y -= 5;
                }
                else if (GameState.PlayerState.direction == Dir.DOWN)
                {
                    newPos.Y += 5;
                }
                else if (GameState.PlayerState.direction == Dir.RIGHT)
                {
                    newPos.X += 5;
                }
                else if (GameState.PlayerState.direction == Dir.LEFT)
                {
                    newPos.X -= 5;
                }
            }
            GameState.PlayerState.position = newPos;
            linkSprite.Update(gt);
        }
        public void LoadContent(ContentManager content)
        {
            linkSprite.LoadContent(content);
        }
        public void Reset() 
        { 
            GameState.PlayerState.position = new Vector2(100, 100);
            GameState.PlayerState.isMoving = false;
            health = 100;
        }
        public void takeDamage(int damage)
        {
            health -= damage;
            if (health <= 0)
            {
               Reset();
            }
        }
        public void gainHealth(int health)
        {
            this.health += health;

            if (this.health > 100)
            {
                this.health = 100;
            }
        }


    }
}
