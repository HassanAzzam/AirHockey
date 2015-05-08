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
        GameApplication App;
        NewGame Game;
        public static Vector2 TopLeft = new Vector2(35.506f, 11.7f);
        private Texture2D Texture;
        public static int Height;
        public static int Width;
        public const float Friction = 0.1F;
        public const float Thickness = 25f;

        public Table(GameApplication App, NewGame game)
            : base(App)
        {
            this.Game = game;
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
            float Radius = this.Game.NewPuck.Radius;
            try
            {
                CPU Cpu = (CPU)PlayerOrCPU;
                if (Position.X == Table.Thickness && Position.Y >= GoalY_Start && Position.Y <= GoalY_End)
                {
                    Position.X = Table.Thickness;
                    return true;
                }
            }
            catch
            {
                if (Position.X == Width - Table.Thickness && Position.Y >= GoalY_Start && Position.Y <= GoalY_End)
                {
                    Position.X = Width - Table.Thickness;
                    return true;
                }
            }

            return false;
        }

        public float GoalY_Start
        {
            get
            {
                return 241 + this.Game.NewPuck.Radius;
            }
        }

        public float GoalY_End
        {
            get
            {
                return 431 - this.Game.NewPuck.Radius;
            }
        }
    }
}