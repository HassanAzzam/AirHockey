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
    class Menu
    {
        //  Member data.
        #region Buttons boundaries.
        private const short StartButtonBeginX = 570;
        private const short StartButtonBeginY = 225;
        private const short StartButtonEndX = 710;
        private const short StartButtonEndY = 280;
        private const short ExitButtonBeginX = 590;
        private const short ExitButtonBeginY = 430;
        private const short ExitButtonEndX = 695;
        private const short ExitButtonEndY = 485;
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
            Vector2 Position = new Vector2();
            Vector2 Origin = new Vector2();
            Color Color = Color.Orange;

            //  Setting background.           

            Game.spriteBatch.Draw(Game.Content.Load<Texture2D>("MenuBackGround"), Game.graphics.GraphicsDevice.Viewport.Bounds, Color.White);

            //  The position where the start button would be drawn.
            Position.X = Game.graphics.GraphicsDevice.Viewport.Width / 2;
            Position.Y = Game.graphics.GraphicsDevice.Viewport.Height / 2 - 100;

            //  The origin of the start button.
            Origin = Game.Font.MeasureString("Start") / 2;

            //  Incase the mouse position is inside the start button.
            if (State.X >= StartButtonBeginX &&
                State.X <= StartButtonEndX &&
                State.Y >= StartButtonBeginY &&
                State.Y <= StartButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }

            //  Drawing the start button.
            Game.spriteBatch.DrawString(Game.Font, "Start", Position, Color, 0, Origin, 0.5f, SpriteEffects.None, 0.5f);

            Color = Color.Orange;

            //  The position where the exit button would be drawn.
            Position.Y += 200;

            //  The origin of the exit button.
            Origin = Game.Font.MeasureString("Exit") / 2;

            //  Incase the mouse position is inside the exit button.
            if (State.X >= ExitButtonBeginX &&
                State.X <= ExitButtonEndX &&
                State.Y >= ExitButtonBeginY &&
                State.Y <= ExitButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }

            //  Drawing the exit button.
            Game.spriteBatch.DrawString(Game.Font, "Exit", Position, Color, 0, Origin, 0.5f, SpriteEffects.None, 0.5f);
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

                //  Exit button pressed.
                if (State.X >= ExitButtonBeginX &&
                State.X <= ExitButtonEndX &&
                State.Y >= ExitButtonBeginY &&
                State.Y <= ExitButtonEndY)
                {
                    return -1;
                }
            }
            return 0;
        }
    }
}