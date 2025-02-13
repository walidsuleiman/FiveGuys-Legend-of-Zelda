using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FiveGuysFixed
{
  public interface IEnemy
  {
    void MoveUp();
    void MoveDown();
    void MoveLeft();
    void MoveRight();
    void Attack();
    void Damage(int damage);
  }
}
