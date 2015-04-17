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
    public class Disc : GameElement
    {
        public Disc(NewGame game):base(game)
        {
            game.Content.RootDirectory = "Content";
            Position = new Vector2(Table.WIDTH / 2 - (float)12.5, Table.LENGTH / 2 - (float)12.5);
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Disc");
            base.LoadContent();
        }

        public Vector2 VECTOR
        {
            get { throw new System.NotImplementedException(); }
            set { }
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft+Position, Color.White);
        }
    }
}
