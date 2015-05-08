using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using System.Diagnostics;
using Microsoft.Xna.Framework.Content;

namespace AirHockey
{
    class MainMenu
    {
        //  Member data.
        #region Buttons boundaries.
        private const short StartButtonBeginX = 610;
        private const short StartButtonBeginY = 260;
        private const short StartButtonEndX = 745;
        private const short StartButtonEndY = 305;
        private const short InstructionsButtonBeginX = 510;
        private const short InstructionsButtonBeginY = 385;
        private const short InstructionsButtonEndX = 845;
        private const short InstructionsButtonEndY = 440;
        private const short ExitButtonBeginX = 620;
        private const short ExitButtonBeginY = 570;
        private const short ExitButtonEndX = 730;
        private const short ExitButtonEndY = 620;
        #endregion

        //  Member methods.
        /// <summary>
        /// Drawing the main menu buttons.
        /// </summary>
        /// <param name="Game"> The game where the main menu would be drawn. </param>
        ///
        public void Draw(ref NewGame Game)
        {
            MouseState State = Mouse.GetState();    //  Getting mouse position.
            Vector2 Position = new Vector2();   //  The position where the button would be drawn.
            Color Color = Color.Red * 1.5f; //  The text color.

            //  Setting background.           
            Game.SpriteBatch.Draw(Game.Content.Load<Texture2D>("MenuBackGround"), Game.Graphics.GraphicsDevice.Viewport.Bounds, Color.White);

            //  Setting start button position.
            Position.X = 675;
            Position.Y = 285;

            //  Incase the mouse position is inside the start button.
            if (State.X >= StartButtonBeginX &&
                State.X <= StartButtonEndX &&
                State.Y >= StartButtonBeginY &&
                State.Y <= StartButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }

            //  Drawing the start button.
            Game.DrawText("Start", Color, Position, 0.5f);

            //  Setting instructions button color.
            Color = Color.Red * 1.5f;

            //  Setting instructions button position.
            Position.X = 675;
            Position.Y = 420;

            //  Incase the mouse position is inside the instructions button.
            if (State.X >= InstructionsButtonBeginX &&
                State.X <= InstructionsButtonEndX &&
                State.Y >= InstructionsButtonBeginY &&
                State.Y <= InstructionsButtonEndY)
            {
                Color = Color.White;    //  Adding vitality by changing the used color.
            }

            //  Drawing the instructions button.
            Game.DrawText("Instructions", Color, Position, 0.5f);

            //  Setting end button color.
            Color = Color.Red * 1.5f;

            //  Setting end button position.
            Position.X = 675;
            Position.Y = 600;

            //  Incase the mouse position is inside the exit button.
            if (State.X >= ExitButtonBeginX &&
                State.X <= ExitButtonEndX &&
                State.Y >= ExitButtonBeginY &&
                State.Y <= ExitButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }

            //  Drawing the exit button.
            Game.DrawText("Exit", Color, Position, 0.5f);
        }

        /// <summary>
        /// The user choise.
        /// </summary>
        /// <returns> 1 if the start button pressed, -1 if the exit button pressed or 0 otherwise. </returns>
        public short GetState()
        {
            MouseState State = Mouse.GetState();

            //  Mouse left button pressed.
            if (State.LeftButton == ButtonState.Pressed)
            {
                //  Start button pressed.
                if (State.X >= StartButtonBeginX &&
                State.X <= StartButtonEndX &&
                State.Y >= StartButtonBeginY &&
                State.Y <= StartButtonEndY)
                {
                    return 1;
                }

                //  Instructions button pressed.
                if (State.X >= InstructionsButtonBeginX &&
                    State.X <= InstructionsButtonEndX &&
                    State.Y >= InstructionsButtonBeginY &&
                    State.Y <= InstructionsButtonEndY)
                {
                    return 2;
                }

                //  Exit button pressed.
                if (State.X >= ExitButtonBeginX &&
                State.X <= ExitButtonEndX &&
                State.Y >= ExitButtonBeginY &&
                State.Y <= ExitButtonEndY)
                {
                    return 3;
                }
            }
            return 0;
        }
    }
}