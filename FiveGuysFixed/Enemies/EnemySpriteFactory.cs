using Microsoft.Xna.Framework;
using System;
using FiveGuysFixed.Sprites;
using FiveGuysFixed.Config;

namespace FiveGuysFixed.Enemies
{
    public sealed class EnemySpriteFactory
    {
        private static EnemySpriteFactory instance;
        private LoadItems data;

        private EnemySpriteFactory() { }

        public static EnemySpriteFactory Instance =>
            instance ??= new EnemySpriteFactory();

        public void Initialize(LoadItems itemData) => data = itemData;

        // ─── basic enemies ───────────────────
        public ISprite CreateGelSprite() => data.getNewItem(data.gel);
        public ISprite CreateKeeseSprite() => data.getNewItem(data.keese);
        public ISprite CreateStalfosSprite() => data.getNewItem(data.stalfos);
        public ISprite CreateTektikeSprite() => data.getNewItem(data.tektike);

        // ─── directional: Goriya / Moblin ───
        public ISprite CreateGoriyaSprite(Vector2 v) =>
            data.getNewItem(Math.Abs(v.Y) > Math.Abs(v.X)
                             ? (v.Y > 0 ? data.downGoriya : data.upGoriya)
                             : (v.X > 0 ? data.rightGoriya : data.leftGoriya));

        public ISprite CreateMoblinSprite(Vector2 v) =>
            data.getNewItem(Math.Abs(v.Y) > Math.Abs(v.X)
                             ? (v.Y > 0 ? data.downMoblin : data.upMoblin)
                             : (v.X > 0 ? data.rightMoblin : data.leftMoblin));

        // ─── directional: Octorok ────────────
        public ISprite CreateOctorokSprite(Vector2 v)
        {
            if (Math.Abs(v.Y) > Math.Abs(v.X))
                return data.getNewItem(v.Y > 0 ? data.octorokDown : data.octorokUp);
            else
                return data.getNewItem(v.X > 0 ? data.octorokRight : data.octorokLeft);
        }

        // ─── boss & projectiles ───────────────
        public ISprite CreateAquamentusSprite() => data.getNewItem(data.Aquamentus);
        public ISprite CreateAquamentusFireballSprite() => data.getNewItem(data.AquamentusAttack);
        public ISprite CreateFireballSprite() => data.getNewItem(data.fireball);
        public ISprite CreateBoomerangSprite() => data.getNewItem(data.boomerang);
    }
}
