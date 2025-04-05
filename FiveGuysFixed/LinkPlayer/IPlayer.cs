using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;

namespace FiveGuysFixed.LinkPlayer
{
    public interface IPlayer
    {

        public void Move(Dir dir);
        public void Idle();
        public void Attack();
        public void SwitchItem();
        public void Draw(SpriteBatch _spriteBatch);
        public void Update(GameTime gt);
        public void LoadContent(ContentManager content);
        public void Reset();
        public void TakeDamage(int damage);
        public void GainHealth(int health);
        public void SetInvincibility(float duration);
        public bool IsInvincible { get; }
    }
}
