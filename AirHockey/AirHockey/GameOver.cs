using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    class GameOver
    {
        public static void Win(GameApplication App)
        {
            Vector2 Position = new Vector2(App.Graphics.GraphicsDevice.Viewport.Width / 2, App.Graphics.GraphicsDevice.Viewport.Height / 2);
            App.UIDesigner.DrawBackground(Color.White, 1f);
            App.UIDesigner.DrawBackground(Color.Green, 0.8f);
            App.UIDesigner.DrawText("YOU WIN!", Color.White, Position, 1f);
        }

        public static void Lose(GameApplication App)
        {
            Vector2 Position = new Vector2(App.Graphics.GraphicsDevice.Viewport.Width / 2, App.Graphics.GraphicsDevice.Viewport.Height / 2);
            App.UIDesigner.DrawBackground(Color.White, 1f);
            App.UIDesigner.DrawBackground(Color.Red, 0.8f);
            App.UIDesigner.DrawText("YOU LOSE!", Color.White, Position, 1f);
        }
    }
}
