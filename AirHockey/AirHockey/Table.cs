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

namespace AirHockey
{
    public class Table : DrawableGameComponent
    {
        private GameApplication App;
        private NewGame Game;
        public static Vector2 TopLeft = new Vector2(35.506f, 11.7f);
        private Texture2D Texture;
        public static int Height;
        public static int Width;
        public const float Friction = 0.1f;
        public const float Thickness = 25f;

        public Table(ref GameApplication App, ref NewGame Game)
            : base(App)
        {
            this.Game = Game;
            this.App = App;
        }

        protected override void LoadContent()
        {
            this.Texture = App.Content.Load<Texture2D>("Table");
            Height = Texture.Height;
            Width = Texture.Width;
            base.LoadContent();
        }

        public void Draw()
        {
            this.App.SpriteBatch.Draw(this.Texture, TopLeft, Color.White);
        }

        public bool CheckGoal(object PlayerOrCPU, Vector2 Position)
        {
            float PuckRadius = this.Game.Puck.Radius;
            try
            {
                CPU Cpu = (CPU)PlayerOrCPU;
                if (Position.X - PuckRadius <= Thickness && Position.Y >= 241 + PuckRadius && Position.Y <= 431 - PuckRadius)
                {
                    return true;
                }
            }
            catch
            {
                if (Position.X + PuckRadius >= Width - Thickness && Position.Y >= 241 + PuckRadius && Position.Y <= 431 - PuckRadius)
                {
                    return true;
                }
            }

            return false;
        }
    }
}