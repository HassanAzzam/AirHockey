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
        Vector2 Goal = new Vector2(96, 184);
        public Disc(NewGame game):base(game)
        {
            RADIUS=12;
            game.Content.RootDirectory = "Content";
            Position = new Vector2(Table.WIDTH / 2, Table.LENGTH / 2);
            Speed = new Vector2(0, 3);
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

        public override void Movement()
        {
            if (Position.X >= Goal.X + RADIUS && Position.X <= Goal.Y - RADIUS && (Position.Y-RADIUS < 15 || Position.Y+RADIUS > Table.LENGTH - 40)) 
                game.Exit();
            base.Movement();
            if (Position.X == game.GameTable.TableTopLeft.X + Table.WIDTH -15 - RADIUS || Position.X == game.GameTable.TableTopLeft.X +15 + RADIUS) 
                Speed.X *= -0.5f;
            if (Position.Y == game.GameTable.TableTopLeft.Y + Table.LENGTH -40 - RADIUS || Position.Y == game.GameTable.TableTopLeft.Y + RADIUS +40) 
                Speed.Y *= -0.5f;

            double Angle = Math.Atan2((Speed.Y), (Speed.X));
            Vector2 F = new Vector2(0,0);
            F = new Vector2((float)(Table.FRICTION * Math.Cos(Angle)), (float)(Table.FRICTION * Math.Sin(Angle)));
            Speed -= F;
        }

    }
}
