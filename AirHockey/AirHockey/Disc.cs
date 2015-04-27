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
           
            game.Content.RootDirectory = "Content";
            Velocity = new Vector2(0, 0);
            Friction = 0;
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Disc");
            RADIUS = TEXTURE.Width / 2;
            Position = new Vector2(Table.WIDTH / 4,20);
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft - new Vector2(RADIUS, RADIUS) + Position, Color.White);
        }
        public void CalculateFriction()
        {
        }
        public override void Move()
        {
            //Alaa's Part :D
            // Velocity *= 0.9f;// Velocity.Y -= 0.1f;
            //Velocity = new Vector2(Velocity.X / Math.Abs(Velocity.X), Velocity.Y / Math.Abs(Velocity.Y));
            BoundPositionInTable(this, Velocity);
            if (Position.X == Table.WIDTH - RADIUS - 20)
            {
                Velocity.X *= -1;
            }
            if (Position.X == RADIUS + 20)
            {
                Velocity.X *= -1;
            }
            if (Position.Y == Table.HEIGHT - RADIUS - 20)
            {
                Velocity.Y *= -1;
            }
            if (Position.Y == RADIUS + 20)
            {
                Velocity.Y *= -1;
            }
        }

    }
}
