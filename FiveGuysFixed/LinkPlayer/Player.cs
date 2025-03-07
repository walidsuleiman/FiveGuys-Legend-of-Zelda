using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection.Metadata;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Collisions;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;
using FiveGuysFixed.Collisions;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer, ICollidable
    {
        private LinkWalkAnimation linkSprite;
        private LinkSwordAnimation swordAnimation;

        public double Rad { get { return Math.Max(linkSprite.Height, linkSprite.Width); } }
        public Vector2 position { get { return GameState.PlayerState.position; } }

        CollisionType ICollidable.type => CollisionType.PLAYER;


        public Player()
        {
            linkSprite = new LinkWalkAnimation();
            swordAnimation = new LinkSwordAnimation();
            GameState.PlayerState.health = 100;

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

        public void attack() 
        {
            if(GameState.PlayerState.heldWeapon != WeaponType.NONE)
            {
                GameState.PlayerState.isAttacking = true;
            }
        }
        public void switchItem() { }
        public void Draw(SpriteBatch _spriteBatch) 
        {
            if (GameState.PlayerState.isAttacking)
            {
                //Add switchcase for other weapons
                swordAnimation.Draw(_spriteBatch);

            }
            else{
                linkSprite.Draw(_spriteBatch, null);
            }

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
            if (GameState.PlayerState.isAttacking)
            {
                swordAnimation.Update(gt);
            } else
            {
                linkSprite.Update(gt);
            }
                
            
        }
        public void LoadContent(ContentManager content)
        {
            linkSprite.LoadContent(content);
            swordAnimation.LoadContent(content);
        }
        public void Reset() 
        { 
            GameState.PlayerState.position = new Vector2(100, 100);
            GameState.PlayerState.isMoving = false;
            GameState.PlayerState.health = 100;
        }
        public void takeDamage(int damage)
        {
            linkSprite.takeDamage();
            GameState.PlayerState.health -= damage;
            if (GameState.PlayerState.health <= 0)
            {
               Reset();
            }
        }
        public void gainHealth(int health)
        {
            GameState.PlayerState.health += health;

            if (GameState.PlayerState.health > 100)
            {
                GameState.PlayerState.health = 100;
            }
        }

        public void onCollision(ICollidable a, ICollidable b)
        {
            if (a.type == CollisionType.ENEMY || b.type == CollisionType.ENEMY)
            {
                takeDamage(15);
            }
            if (a.type == CollisionType.ENEMY || b.type == CollisionType.ENEMY)
            {
                Vector2 previousPosition = GameState.PlayerState.position;

                if (GameState.PlayerState.direction == Dir.UP)
                {
                    previousPosition.Y += 5;
                }
                else if (GameState.PlayerState.direction == Dir.DOWN)
                {
                    previousPosition.Y -= 5;
                }
                else if (GameState.PlayerState.direction == Dir.LEFT)
                {
                    previousPosition.X += 5;
                }
                else if (GameState.PlayerState.direction == Dir.RIGHT)
                {
                    previousPosition.X -= 5;
                }

                GameState.PlayerState.position = previousPosition;
            }
        }
    }
    
}
