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
        

        private float Friction;
        public Disc(NewGame game):base(game)
        {
            RADIUS=12;
            game.Content.RootDirectory = "Content";
            Position = new Vector2(Table.WIDTH / 2, Table.LENGTH / 2);
            Velocity = new Vector2(0, 0);
            Friction = 0;
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Disc");
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft - new Vector2(RADIUS, RADIUS) + Position, Color.White);
        }
        public void CalculateFriction()
        {
        }
        public override void Movement()
        {
            //Alaa's Part :D

        }

    }
}
