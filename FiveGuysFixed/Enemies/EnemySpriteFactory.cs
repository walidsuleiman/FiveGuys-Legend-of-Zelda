using Microsoft.Xna.Framework;
using System;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Config;

using Microsoft.Xna.Framework;
using FiveGuysFixed.Config;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Animation;

namespace FiveGuysFixed.Enemies
{
    /// <summary>
    /// Singleton factory that creates enemy sprites using dedicated loaders.
    /// </summary>
    public sealed class EnemySpriteFactory
    {
        private static EnemySpriteFactory instance;
        private ProjectileLoader _projectiles;
        private EnemyLoader _enemies;
        private BossLoader _bosses;

        private EnemySpriteFactory() { }

        public static EnemySpriteFactory Instance
            => instance ??= new EnemySpriteFactory();

        /// <summary>
        /// Initialize with specific loaders instead of a monolithic LoadItems.
        /// </summary>
        public void Initialize(
            ProjectileLoader projectiles,
            EnemyLoader enemies,
            BossLoader bosses)
        {
            _projectiles = projectiles;
            _enemies = enemies;
            _bosses = bosses;
        }

        // helper to convert ItemData into ISprite
        private ISprite CreateSprite(ItemData d)
            => new Sprite(d.Texture, d.X, d.Y, d.Dimensions, d.Dimensions, d.Frames);

        // ─── basic enemies ───────────────────
        public ISprite CreateGelSprite() => CreateSprite(_enemies.Gel);
        public ISprite CreateKeeseSprite() => CreateSprite(_enemies.Keese);
        public ISprite CreateStalfosSprite() => CreateSprite(_enemies.Stalfos);

        // ─── directional: Goriya / Moblin ───
        public ISprite CreateGoriyaSprite(Vector2 v)
            => CreateSprite(
                System.Math.Abs(v.Y) > System.Math.Abs(v.X)
                    ? (v.Y > 0 ? _enemies.DownGoriya : _enemies.UpGoriya)
                    : (v.X > 0 ? _enemies.RightGoriya : _enemies.LeftGoriya)
            );

        public ISprite CreateMoblinSprite(Vector2 v)
            => CreateSprite(
                System.Math.Abs(v.Y) > System.Math.Abs(v.X)
                    ? (v.Y > 0 ? _enemies.DownMoblin : _enemies.UpMoblin)
                    : (v.X > 0 ? _enemies.RightMoblin : _enemies.LeftMoblin)
            );

        // ─── directional: Octorok ────────────
        public ISprite CreateOctorokSprite(Vector2 v)
        {
            ItemData d = System.Math.Abs(v.Y) > System.Math.Abs(v.X)
                ? (v.Y > 0 ? _enemies.DownOctorok : _enemies.UpOctorok)
                : (v.X > 0 ? _enemies.RightOctorok : _enemies.LeftOctorok);
            return CreateSprite(d);
        }
        public ISprite CreateTektikeSprite() => CreateSprite(_enemies.Tektike);

        // ─── boss & projectiles ───────────────
        public ISprite CreateAquamentusSprite() => CreateSprite(_bosses.Aquamentus);
        public ISprite CreateAquamentusFireballSprite() => CreateSprite(_bosses.AquamentusAttack);
        public ISprite CreateFireballSprite() => CreateSprite(_projectiles.Fireball);
        public ISprite CreateBoomerangSprite() => CreateSprite(_projectiles.Boomerang);
    }
}
