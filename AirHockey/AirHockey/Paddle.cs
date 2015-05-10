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
    public abstract class Paddle : GameElement
    {
        public Paddle(ref GameApplication App, ref NewGame Game)
            : base(ref App, ref Game)
        {
            this.Mass = 0.135f;
            this.Position = new Vector2(0, 0);
        }


        protected override void LoadContent()
        {
            this.Texture = App.Content.Load<Texture2D>("Paddle");
            this.Radius = this.Texture.Width / 2;
            base.LoadContent();
        }

        public void Draw()
        {
            App.SpriteBatch.Draw(this.Texture, Table.TopLeft + this.Position - new Vector2(this.Radius, this.Radius), Color.White);
        }
    }
}