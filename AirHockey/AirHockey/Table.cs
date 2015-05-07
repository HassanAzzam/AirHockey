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
        private Texture2D TEXTURE;
        public static int HEIGHT;
        public static int WIDTH;
        public static Vector2 TopLeft = new Vector2(35.506f, 11.7f);
        public const float FRICTION=0.1F;
        public const float Thickness = 25f;
        NewGame game;

        public Table(NewGame game) : base(game)
        {
            this.game = game;
            game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Table");
            HEIGHT=TEXTURE.Height;
            WIDTH=TEXTURE.Width;
            base.LoadContent();
        }

        public void Draw(){
            game.spriteBatch.Draw(this.TEXTURE, TopLeft, Color.White);
        }

        public bool CheckGoal(object PLAYERorCPU, Vector2 Position)
        {
            float PuckRadius = game.NewPuck.RADIUS;
            try
            {
                CPU Cpu = (CPU)PLAYERorCPU;
                if (Position.X - PuckRadius <= Thickness && Position.Y >= 241 + PuckRadius && Position.Y <= 431 - PuckRadius)
                {
                    return true;
                }
            }
            catch
            {
                if (Position.X + PuckRadius >= WIDTH - Thickness && Position.Y >= 241 + PuckRadius && Position.Y <= 431 - PuckRadius)
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}
