using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class UI
    {
        GameApplication App;

        public UI(GameApplication App)
        {
            this.App = App;
        }

        public void DrawText(string Text, Color Color, Vector2 Position, float Scale)
        {
            Vector2 Origin = this.App.Font.MeasureString(Text) / 2;
            this.App.SpriteBatch.DrawString(this.App.Font, Text, Position, Color, 0, Origin, Scale, SpriteEffects.None, 0.5f);
        }

        public void DrawBackground(Color Color, float Alpha)
        {
            Texture2D Rectangle = new Texture2D(this.App.Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            Rectangle.SetData<Color>(new[] { Color * Alpha });
            this.App.SpriteBatch.Draw(Rectangle, new Rectangle(0, 0, this.App.Graphics.GraphicsDevice.Viewport.Width, this.App.Graphics.GraphicsDevice.Viewport.Height), Color.White);
        }
    }
}
