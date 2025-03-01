using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FiveGuysFixed.Collisions
{
    class CollisionManager
    {

        List<ICollidable> ObjectList;
        public CollisionManager()
        {
            this.ObjectList = new List<ICollidable>();
        }

        public void Register(ICollidable a)
        {
            ObjectList.Add(a);
        }

        public void unRegister(ICollidable a)
        {
            ObjectList.Remove(a);
        }
        public void DetectCollision()
        {
            for(int i = 0; i < ObjectList.Count - 1; i++)
            {
                for(int j = i + 1; j < ObjectList.Count; j++)
                {
                    bool collide = hasCollide(ObjectList[i], ObjectList[j]);
                    if (collide)
                    {
                        ObjectList[i].onCollision(ObjectList[i], ObjectList[j]);
                        ObjectList[j].onCollision(ObjectList[j], ObjectList[i]);
                    }

                }
            }
        }
        private bool hasCollide(ICollidable a, ICollidable b)
        {
            double distance = (b.position.X - a.position.X) * (b.position.X - a.position.X) + (b.position.Y - a.position.Y) * (b.position.Y - a.position.Y);
            double radsum = (a.Rad + b.Rad) * (a.Rad + b.Rad);

            if (distance <= radsum)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
