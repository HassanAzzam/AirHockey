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
        public Paddle(NewGame game) : base(game)
        {
            Mass = 0.135f;
            Position = new Vector2(0, 0);
            game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Paddle");
            RADIUS = TEXTURE.Width / 2;
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, Table.TopLeft + this.Position - new Vector2(RADIUS, RADIUS), Color.White);
        }

    }
}
