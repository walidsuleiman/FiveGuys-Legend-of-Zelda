using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.Commands;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace FiveGuysFixed.LinkPlayer
{
    public class Player : IPlayer
    {

        private Vector2 position;
        private Vector2 movement;
        private bool isMoving;

        public void moveUp()
        {
            movement = new Vector2(0, -1);
            isMoving = true;
        }
        public void moveDown() 
        { 
            movement = new Vector2(0, 1);
            isMoving = true;
        }
        public void moveLeft() 
        {
            movement = new Vector2(-1, 0);
            isMoving = true;
        }
        public void moveRight() 
        {
            movement = new Vector2(1, 0);
            isMoving = true;
        }
        public void attack() { }
        public void switchItem() { }
        public void Draw(SpriteBatch spriteBatch, Vector2 setPos) { }
        public void Update()
        {
        if(isMoving)
            {
                position += movement;
                isMoving = false;
            }
        }
        public void Reset() 
        { 
            position = new Vector2(0, 0);
            isMoving = false;
        }
        public void takeDamage(int damage) { }
        public void gainHealth(int health) { }

        public Player(Vector2 setPos)
        {
            position = setPos;
            movement = new Vector2(0, 0);
            isMoving = false;
        }

    }
}
