using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.GUI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
namespace FiveGuysFixed.HUD
{
    public class HUD
    {
        private Hearts hearts;
        private RupeeCount rupees;
        private MiniMap miniMap;
        Texture2D blackPixel;

        public HUD()
        {
            hearts = new Hearts();
            rupees = new RupeeCount();
        }

        public void Update(GameTime gameTime)
        {
            hearts.Update(gameTime);
            rupees.Update(gameTime);
            miniMap?.Update(gameTime);
        }

        public void Draw(SpriteBatch spritebatch)
        {
            this.blackPixel = new Texture2D(spritebatch.GraphicsDevice, 1, 1);
            this.blackPixel.SetData(new[] { Color.SlateGray });
            spritebatch.Draw(blackPixel, new Rectangle(0, 720, 1280, 220), Color.SlateGray);

            hearts.Draw(spritebatch);
            rupees.Draw(spritebatch);

            
            if (miniMap == null)
            {
                miniMap = new MiniMap(
                    spritebatch.GraphicsDevice,
                    new Vector2(GameState.WindowWidth - 260, 740), // position in bottom-right
                    160, // width
                    160  // height
                );


                miniMap.LoadContent(GameState.contentLoader.miniMapTexture);
            }

            miniMap.Draw(spritebatch);
        }
    }
}