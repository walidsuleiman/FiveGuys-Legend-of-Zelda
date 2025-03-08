using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FiveGuysFixed.GameStates;
using System.Diagnostics;
using System.Collections.Generic;

namespace FiveGuysFixed.HUD
{
    public class RupeeCount : IHUD
    {
        private int greenRupeeCount;
		private int redRupeeCount;
		//private List<Rectangle> rupeeRectangles = new List<Rectangle>();

		private Rectangle greenRupeeRectangle = new Rectangle(35, 2, 14, 26);
		private Rectangle redRupeeRectangle = new Rectangle(35, 58, 14, 26);

		private Rectangle xRectangle = new Rectangle(519, 117, 8, 8);
		private Rectangle zeroRectangle = new Rectangle(519 + (9 * 1), 117, 8, 8);
		private Rectangle oneRectangle = new Rectangle(519 + (9 * 2), 117, 8, 8);
		private Rectangle twoRectangle = new Rectangle(519 + (9 * 3), 117, 8, 8);
		private Rectangle threeRectangle = new Rectangle(519 + (9 * 4), 117, 8, 8);
		private Rectangle fourRectangle = new Rectangle(519 + (9 * 5), 117, 8, 8);
		private Rectangle fiveRectangle = new Rectangle(519 + (9 * 6), 117, 8, 8);
		private Rectangle sixRectangle = new Rectangle(519 + (9 * 7), 117, 8, 8);
		private Rectangle sevenRectangle = new Rectangle(519 + (9 * 8), 117, 8, 8);
		private Rectangle eightRectangle = new Rectangle(519 + (9 * 9), 117, 8, 8);
		private Rectangle nineRectangle = new Rectangle(519 + (9 * 10), 117, 8, 8);
		
		Vector2 greenRupeePos = new Vector2(1200, 40);
		Vector2 xPosGreen = new Vector2(1216, 43);
        Vector2 digitOnePosGreen = new Vector2(1232, 43);
		//Vector2 digitTwoPos = new Vector2(1248, 40);

		Vector2 redRupeePos = new Vector2(1200, 80);
		Vector2 xPosRed = new Vector2(1216, 83);
		Vector2 digitOnePosRed = new Vector2(1232, 83);

		public RupeeCount() {}

        public void Draw(SpriteBatch spriteBatch)
        {
			switch (GameState.PlayerState.greenRupees = greenRupeeCount)
			{
				case 0:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, zeroRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 1:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, oneRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 2:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, twoRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 3:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, threeRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 4:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, fourRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 5:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, fiveRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 6:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, sixRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 7:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, sevenRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 8:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, eightRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 9:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, greenRupeePos, greenRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosGreen, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosGreen, nineRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
			}

			switch (GameState.PlayerState.redRupees = redRupeeCount)
			{
				case 0:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, zeroRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 1:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, oneRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 2:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, twoRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 3:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, threeRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 4:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, fourRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 5:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, fiveRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 6:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, sixRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 7:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, sevenRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 8:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, eightRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
				case 9:
					spriteBatch.Draw(GameState.contentLoader.rupeeTexture, redRupeePos, redRupeeRectangle, Color.White, 0, new Vector2(0, 0), 0.75f, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, xPosRed, xRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					spriteBatch.Draw(GameState.contentLoader.HudTexture, digitOnePosRed, nineRectangle, Color.White, 0, new Vector2(0, 0), 2, SpriteEffects.None, 0f);
					break;
			}

		}

        public void Update(GameTime gametime)
        {
			greenRupeeCount = GameState.PlayerState.greenRupees;
			redRupeeCount = GameState.PlayerState.redRupees;
		}

	}
}
