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
using FiveGuysFixed.Projectiles;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {
        private LinkWalkAnimation linkSprite;
        private LinkSwordAnimation swordAnimation;

        private SoundEffect harmedSound; // harmed sound
        private SoundEffect hitSound;    // hit sound
        public SoundEffect ItemPickupSound; // item pickup sound


        private bool isInvincible;
        private float invincibilityTimer;

        //private bool canThrowBoomerang = true;
        //private float boomerangCooldown = 0f;
        //private const float BOOMERANG_COOLDOWN_TIME = 0.5f;

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
            if (GameState.PlayerState.isAttacking)
                return; // Already attacking

            GameState.PlayerState.isAttacking = true;
            hitSound.Play(volume: 0.3f, pitch: 0.0f, pan: 0.0f);

            //    // Check if we can throw a boomerang
            //    if (canThrowBoomerang)
            //    {
            //        // Create boomerang velocity based on direction
            //        Vector2 velocity = Vector2.Zero;
            //        Vector2 startPos = GameState.PlayerState.position;

            //        switch (GameState.PlayerState.direction)
            //        {
            //            case Dir.UP:
            //                velocity = new Vector2(0, -5);
            //                startPos.Y -= 20;
            //                break;
            //            case Dir.DOWN:
            //                velocity = new Vector2(0, 5);
            //                startPos.Y += 20;
            //                break;
            //            case Dir.LEFT:
            //                velocity = new Vector2(-5, 0);
            //                startPos.X -= 20;
            //                break;
            //            case Dir.RIGHT:
            //                velocity = new Vector2(5, 0);
            //                startPos.X += 20;
            //                break;
            //        }

            //        // Check if there's already a boomerang
            //        bool boomerangExists = false;
            //        foreach (var projectile in
            //        .Projectiles)
            //        {
            //            if (projectile is Boomerang)
            //            {
            //                boomerangExists = true;
            //                break;
            //            }
            //        }

            //        // Only throw if no boomerang exists
            //        if (!boomerangExists)
            //        {
            //            // Create and add boomerang
            //            try
            //            {
            //                Texture2D weaponTexture = game.Content.Load<Texture2D>("linkSprite");
            //                GameState.roomManager.getCurrentRoom().Projectiles.Add(
            //                    new Boomerang(
            //                        weaponTexture,
            //                        startPos.X,
            //                        startPos.Y,
            //                        velocity
            //                    )
            //                );

            //                // Set cooldown
            //                canThrowBoomerang = false;
            //                boomerangCooldown = BOOMERANG_COOLDOWN_TIME;
            //            }
            //            catch (Exception ex)
            //            {
            //                // Handle exception (texture not found, etc.)
            //                System.Diagnostics.Debug.WriteLine("Boomerang creation error: " + ex.Message);
            //            }
            //        }
            //    }
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
            //if (!canThrowBoomerang)
            //{
            //    boomerangCooldown -= (float)gt.ElapsedGameTime.TotalSeconds;
            //    if (boomerangCooldown <= 0)
            //    {
            //        canThrowBoomerang = true;
            //    }
            //}


        }
        public void LoadContent(ContentManager content)
        {
            linkSprite.LoadContent(content);
            swordAnimation.LoadContent(content);
            harmedSound = content.Load<SoundEffect>("harmed");
            hitSound = content.Load<SoundEffect>("hit");
            ItemPickupSound = content.Load<SoundEffect>("item-pick-up");
        }
        public void Reset()
        {
            GameState.PlayerState.position = new Vector2(GameState.WindowWidth / 2, GameState.WindowHeight / 2);
            GameState.PlayerState.isMoving = false;
            GameState.PlayerState.isAttacking = false;
            GameState.PlayerState.health = 6;
            GameState.PlayerState.greenRupees = 0;
            GameState.PlayerState.redRupees = 0;
            GameState.roomManager.SwitchRoom(1);
        }
        public void TakeDamage(int damage)
        {
            // check if key Z is pressed - don't take damage while throwing boomerang
            if (isInvincible || Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Z))
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
