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
using Microsoft.Xna.Framework.Audio;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {
        private static CollisionHandler collisionHandler = new CollisionHandler();
        private LinkWalkAnimation linkSprite;
        private LinkSwordAnimation swordAnimation;

        private SoundEffect harmedSound; // harmed sound
        private SoundEffect hitSound;    // hit sound

        private bool isInvincible;
        private float invincibilityTimer;

        public bool IsInvincible => isInvincible;
        public double Rad { get { return Math.Max(linkSprite.Height, linkSprite.Width); } }
        public Vector2 position { get { return GameState.PlayerState.position; } }

        private Game1 game;
        public Player(Game1 game)
        {
            this.game = game;
            linkSprite = new LinkWalkAnimation();
            swordAnimation = new LinkSwordAnimation();
            GameState.PlayerState.health = 6;

        }
        public void Move(Dir newDir)
        {
            GameState.PlayerState.direction = newDir;
            linkSprite.animate();
            GameState.PlayerState.isMoving = true;
            linkSprite.facingDirection(newDir);
        }
        public void Idle()
        {
            GameState.PlayerState.isMoving = false;
            linkSprite.idle();
        }

        public void Attack()
        {
            if (GameState.PlayerState.heldWeapon != WeaponType.NONE)
            {
                GameState.PlayerState.isAttacking = true;
                hitSound.Play();
            }
        }
        public void SwitchItem() { }
        public void Draw(SpriteBatch _spriteBatch)
        {
            if (GameState.PlayerState.isAttacking)
            {
                //Add switchcase for other weapons
                swordAnimation.Draw(_spriteBatch);

            }
            else
            {
                Vector2 drawPos = GameState.PlayerState.position + GameState.PlayerState.transitionOffset;
                linkSprite.Draw(_spriteBatch, drawPos);
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
            }
            else
            {
                linkSprite.Update(gt);
            }

            if (isInvincible)
            {
                // Update the invincibility timer
                invincibilityTimer -= (float)gt.ElapsedGameTime.TotalSeconds;
                if (invincibilityTimer <= 0)
                {
                    isInvincible = false; // End invincibility
                }
            }


        }
        public void LoadContent(ContentManager content)
        {
            linkSprite.LoadContent(content);
            swordAnimation.LoadContent(content);
            harmedSound = content.Load<SoundEffect>("harmed");
            hitSound = content.Load<SoundEffect>("hit");
        }
        public void Reset()
        {
            GameState.PlayerState.position = new Vector2(GameState.WindowWidth / 2, GameState.WindowHeight / 2);
            GameState.PlayerState.isMoving = false;
            GameState.PlayerState.isAttacking = false;
            GameState.PlayerState.health = 6;
            GameState.PlayerState.greenRupees = 0;
            GameState.PlayerState.redRupees = 0;
            GameState.roomManager.SwitchRoom(GameState.currentRoomID);
        }
        public void TakeDamage(int damage)
        {
            if (isInvincible)
            {
                return;
            }

            linkSprite.takeDamage();
            GameState.PlayerState.health -= damage;
            harmedSound.Play();
            if (GameState.PlayerState.health <= 0)
            {
                GameStateManager.SetState(new GameOverState(game));
            }
        }
        public void Heal(int healing)
        {
            linkSprite.heal();
            GameState.PlayerState.health += healing;
            if (GameState.PlayerState.health >= 6)
            {
                GameState.PlayerState.health = 6;
            }
        }
        public void GainHealth(int health)
        {
            GameState.PlayerState.health += health;

            if (GameState.PlayerState.health > 6)
            {
                GameState.PlayerState.health = 6;
            }
        }

        public void SetInvincibility(float duration)
        {
            isInvincible = true;
            invincibilityTimer = duration;
        }
    }

}
