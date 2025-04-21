using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;
using System.Linq;


namespace FiveGuysFixed.Weapons___Items
{
    internal class BombPlaced : Item
    {
        private const float COUNTDOWN = 3f;
        private const float BLINK_INTERVAL = .5f;
        private const float ANIM_SPEED = .08f;
        private const int RADIUS = 150;
        private const int DMG = 2;
        private const float SCALE_IDLE = 5f;


        private static readonly Rectangle[] EXP_FRAMES =
        {
            new Rectangle(129,185,16,16),
            new Rectangle(138,185,16,16),
            new Rectangle(155,185,16,16),
            new Rectangle(172,185,16,16)
        };


        private readonly Vector2 pixelPos;
        private readonly Texture2D spriteSheet;

        private float timer = COUNTDOWN, blinkT = 0f, animT = 0f;
        private bool exploded = false, visible = true;
        private int frame = 0;


        public BombPlaced(Texture2D sheet, Vector2 worldPixel)
            : base(sheet, 129, 185,
                   (int)(worldPixel.X / 80f), (int)(worldPixel.Y / 80f),
                   8, 16, 5f)
        {
            pixelPos = worldPixel;
            spriteSheet = sheet;
            ContentLoader.BombSound.Play();
        }


        public override void Update(GameTime gt)
        {
            float dt = (float)gt.ElapsedGameTime.TotalSeconds;

            if (!exploded)
            {
                timer -= dt;
                blinkT += dt;
                if (blinkT >= BLINK_INTERVAL)
                { blinkT = 0f; visible = !visible; }

                if (timer <= 0f)
                {
                    exploded = true;
                    ContentLoader.ExplosionSound.Play();
                    DealDamage();
                }
            }
            else
            {
                animT += dt;
                if (animT >= ANIM_SPEED && frame < EXP_FRAMES.Length - 1)
                { animT = 0f; frame++; }
            }
        }


        public override void Draw(SpriteBatch sb)
        {
            DrawInternal(sb, Vector2.Zero);
        }
        public override void Draw(SpriteBatch sb, Vector2 off)
        {
            DrawInternal(sb, off);
        }

        private void DrawInternal(SpriteBatch sb, Vector2 off)
        {
            if (!exploded)
            {
                if (visible)
                    sb.Draw(spriteSheet,
                            pixelPos + off,
                            new Rectangle(129, 185, 8, 16),
                            Color.White, 0f, Vector2.Zero,
                            SCALE_IDLE, SpriteEffects.None, 0f);
            }
            else if (frame < EXP_FRAMES.Length)
            {
                sb.Draw(spriteSheet,
                        pixelPos + off - new Vector2(8),
                        EXP_FRAMES[frame], Color.White, 0f, Vector2.Zero,
                            SCALE_IDLE, SpriteEffects.None, 0f);
            }
        }

        public override void Use() { }


        public bool IsFinished => exploded && frame >= EXP_FRAMES.Length - 1;


        private void DealDamage()
        {
            if (Vector2.Distance(pixelPos, GameState.PlayerState.position) < RADIUS)
            {
                GameState.Player.TakeDamage(DMG);
            }

            foreach (var e in GameState.currentRoomContents.Enemies.ToList())
            {
                if (Vector2.Distance(pixelPos, e.Position) < RADIUS)
                {
                    e.TakeDamage(DMG * 10);
                }
            }
        }
    }
}
