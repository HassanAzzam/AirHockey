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
    class InstructionsWindow
    {
        //  Member data.
        #region Button boundaries.
        private const short BackButtonBeginX = 605;
        private const short BackButtonBeginY = 495;
        private const short BackButtonEndX = 745;
        private const short BackButtonEndY = 545;
        #endregion

        //  Member methods.
        /// <summary>
        /// Drawing the instructions and the back button.
        /// </summary>
        /// <param name="Application"> The application where the instructions window would be drawn. </param>
        public void Draw(ref GameApplication Application)
        {
            MouseState State = Mouse.GetState();    //  Getting mouse position.
            Vector2 Position = new Vector2();   //  Position where instructions would be written.
            Color Color = Color.DarkBlue * 2f; //  The text color.

            #region Background.
            Application.SpriteBatch.Draw(
                Application.Content.Load<Texture2D>("MenuBackGround"),
                Application.Graphics.GraphicsDevice.Viewport.Bounds,
                Color.White);
            #endregion

            #region Esc.
            Position.X = 575;
            Position.Y = 275;
            Application.UI.DrawText("P", Color, Position, 0.252f);
            #endregion

            #region M.
            Position.Y += 75;
            Application.UI.DrawText("M", Color, Position, 0.252f);
            #endregion

            #region Alt + F4.
            Position.Y += 75;
            Application.UI.DrawText("Alt + F4", Color, Position, 0.252f);
            #endregion

            #region Pause.
            Position.X += 200;
            Position.Y = 275;
            Application.UI.DrawText("Pause", Color, Position, 0.252f);
            #endregion

            #region Mute / Unmute.
            Position.Y += 75;
            Application.UI.DrawText("Mute / Unmute", Color, Position, 0.252f);
            #endregion

            #region Exit.
            Position.Y += 75;
            Application.UI.DrawText("Exit", Color, Position, 0.252f);
            #endregion

            #region Back.
            //  Setting color.
            Color = Color.Red *1.5f;
            //  Setting position.
            Position.X = 675;
            Position.Y = 525;
            //  Incase the mouse position is inside the button.
            if (State.X >= BackButtonBeginX &&
                State.X <= BackButtonEndX &&
                State.Y >= BackButtonBeginY &&
                State.Y <= BackButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }
            //  Drawing the button.
            Application.UI.DrawText("Back", Color, Position, 0.5f);
            #endregion
        }
        
        /// <summary>
        /// The user choise.
        /// </summary>
        /// <returns> True if the back button pressed or false otherwise. </returns>
        public bool GetState()
        {
            MouseState State = Mouse.GetState();

            //  Mouse left button pressed.
            if (State.LeftButton == ButtonState.Pressed)
            {
                //  Back button pressed.
                if (State.X >= BackButtonBeginX &&
                    State.X <= BackButtonEndX &&
                    State.Y >= BackButtonBeginY &&
                    State.Y <= BackButtonEndY)
                {
                    return true;
                }
            }
            return false;
        }
    }
}