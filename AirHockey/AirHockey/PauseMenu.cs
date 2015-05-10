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
    class PauseMenu
    {
        //  Member data.
        #region Buttons boundaries.
        private const short ResumeButtonBeginX = 555;
        private const short ResumeButtonBeginY = 170;
        private const short ResumeButtonEndX = 800;
        private const short ResumeButtonEndY = 220;
        private const short MainMenuButtonBeginX = 515;
        private const short MainMenuButtonBeginY = 470;
        private const short MainMenuButtonEndX = 835;
        private const short MainMenuButtonEndY = 515;
        #endregion

        //  Member methods.
        /// <summary>
        /// Drawing the pause menu buttons.
        /// </summary>
        /// <param name="Application"> The application where the pause menu would be drawn. </param>
        public void Draw(ref GameApplication Application)
        {
            MouseState State = Mouse.GetState();    //  Getting mouse position.
            Vector2 Position = new Vector2();   //  The position where the button would be drawn.
            Color Color; //  The text color.

            #region Background.
            //  Setting color.
            Color = Color.Black;
            Application.UI.DrawBackground(Color, 0.5f);
            #endregion

            #region Resume.
            //  Setting color.
            Color = Color.Red * 1.5f;
            //  Setting position.
            Position.X = 675;
            Position.Y = 200;
            //  Incase the mouse position is inside the button.
            if (State.X >= ResumeButtonBeginX &&
                State.X <= ResumeButtonEndX &&
                State.Y >= ResumeButtonBeginY &&
                State.Y <= ResumeButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }
            //  Drawing the button.
            Application.UI.DrawText("Resume", Color, Position, 0.5f);
            #endregion

            #region Main menu.
            //  Setting color.
            Color = Color.Red * 1.5f;
            //  Setting position.
            Position.X = 675;
            Position.Y = 500;
            //  Incase the mouse position is inside the button.
            if (State.X >= MainMenuButtonBeginX &&
                State.X <= MainMenuButtonEndX &&
                State.Y >= MainMenuButtonBeginY &&
                State.Y <= MainMenuButtonEndY)
            {
                Color = Color.White;   //  Adding vitality by changing the used color.
            }
            //  Drawing the button.
            Application.UI.DrawText("Main menu", Color, Position, 0.5f);
            #endregion
        }

        /// <summary>
        /// The user choise.
        /// </summary>
        /// <returns> 1 if the resume button pressed,
        /// 2 if the main menu button pressed
        /// or  otherwise. </returns>
        public short GetState()
        {
            MouseState State = Mouse.GetState();

            //  Mouse left button pressed.
            if (State.LeftButton == ButtonState.Pressed)
            {
                //  Resume button pressed.
                if (State.X >= ResumeButtonBeginX &&
                    State.X <= ResumeButtonEndX &&
                    State.Y >= ResumeButtonBeginY &&
                    State.Y <= ResumeButtonEndY)
                {
                    return 1;
                }

                //  Main menu button pressed.
                if (State.X >= MainMenuButtonBeginX &&
                    State.X <= MainMenuButtonEndX &&
                    State.Y >= MainMenuButtonBeginY &&
                    State.Y <= MainMenuButtonEndY)
                {
                    return 2;
                }
            }
            return 0;
        }
    }
}