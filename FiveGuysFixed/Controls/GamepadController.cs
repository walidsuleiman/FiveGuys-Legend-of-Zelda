using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using FiveGuys.Controls;
using FiveGuysFixed.Animation;
using FiveGuysFixed.Commands;
using FiveGuysFixed.Common;
using FiveGuysFixed.GameStates;
using FiveGuysFixed.LinkPlayer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace FiveGuysFixed.Controls
{
    public class GamepadController : IController
    {
        private GamePadState previousState;
        private GamePadState currentState;
        private Game1 game;

        public GamepadController(Game1 game)
        {
            this.game = game;
            this.currentState = GamePad.GetState(PlayerIndex.One);
            this.previousState = GamePad.GetState(PlayerIndex.One);

        }

        public void Update()
        {
            currentState = GamePad.GetState(PlayerIndex.One);


            Buttons[] movementButtons = { Buttons.DPadUp, Buttons.DPadDown, Buttons.DPadLeft, Buttons.DPadRight, Buttons.LeftThumbstickUp, Buttons.LeftThumbstickDown, Buttons.LeftThumbstickLeft, Buttons.LeftThumbstickRight };
            Buttons[] actionButtons = { Buttons.A, Buttons.B, Buttons.X, Buttons.Y };
            Buttons[] shoulderButtons = { Buttons.LeftShoulder, Buttons.RightShoulder };
            Buttons[] triggerButtons = { Buttons.LeftTrigger, Buttons.RightTrigger };
            Buttons[] gameButtons = { Buttons.Start, Buttons.Back };


            foreach (Buttons mButton in movementButtons)
            {
                bool currentDown = currentState.IsButtonDown(mButton);
                bool previousDown = previousState.IsButtonDown(mButton);

                // Determine if the Button was just pressed or just released.
                if (!currentDown && previousDown || !currentDown && previousDown)
                {
                    bool isButtonDown = currentDown;

                    switch (mButton)
                    {
                        case Buttons.DPadUp:
                        case Buttons.LeftThumbstickUp:
                            new MovementCommand(game, Dir.UP, isButtonDown).Execute();
                            break;

                        case Buttons.DPadDown:
                        case Buttons.LeftThumbstickDown:
                            new MovementCommand(game, Dir.DOWN, isButtonDown).Execute();
                            break;

                        case Buttons.DPadLeft:
                        case Buttons.LeftThumbstickLeft:
                            new MovementCommand(game, Dir.LEFT, isButtonDown).Execute();
                            break;
                        case Buttons.DPadRight:
                        case Buttons.LeftThumbstickRight:
                            new MovementCommand(game, Dir.RIGHT, isButtonDown).Execute();
                            break;

                    }
                }
            }

            foreach (Buttons aButton in actionButtons)
            {
                if (currentState.IsButtonDown(aButton))
                {
                    switch (aButton)
                    {
                        case Buttons.A:
                            GameState.Player.attack();
                            break;
                        case Buttons.B:
                            GameState.Player.takeDamage(15);
                            break;
                        case Buttons.X:
                            new ItemSwitchCommand(game, true).Execute();
                            break;
                        case Buttons.Y:
                            new ItemSwitchCommand(game, false).Execute();
                            break;
                    }
                }
            }

            foreach (Buttons gButton in gameButtons)
            {
                if (currentState.IsButtonDown(gButton))
                {
                    switch (gButton)
                    {
                        case Buttons.Start:
                            //Start Gane
                            break;

                        case Buttons.Back:
                            game.Exit();
                            break;

                    }
                }
            }

            foreach (Buttons sButton in shoulderButtons)
            {
                if (currentState.IsButtonDown(sButton))
                {
                    switch (sButton)
                    {
                        case Buttons.LeftShoulder:
                            //Left Shoulder Action
                            break;

                        case Buttons.RightShoulder:
                            //Right Shouder Action
                            break;

                    }
                }
            }

            foreach (Buttons tButton in triggerButtons)
            {
                if (currentState.IsButtonDown(tButton))
                {
                    switch (tButton)
                    {
                        case Buttons.LeftTrigger:
                            //Left Trigger Action
                            break;

                        case Buttons.RightTrigger:
                            //Right Trigger Action
                            break;

                    }
                }
            }

            previousState = currentState;

        }
    }
}

