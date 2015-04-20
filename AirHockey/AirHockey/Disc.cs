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
            RADIUS=12;
            game.Content.RootDirectory = "Content";
            Position = new Vector2(Table.WIDTH / 2, Table.LENGTH / 2);
            Speed = new Vector2(3, 7);
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
            base.Movement();
            if (Position.X == game.GameTable.TableTopLeft.X + Table.WIDTH -15 - RADIUS || Position.X == game.GameTable.TableTopLeft.X +15 + RADIUS) 
                Speed.X *= -0.5F;
            if (Position.Y == game.GameTable.TableTopLeft.Y + Table.LENGTH -45 - RADIUS || Position.Y == game.GameTable.TableTopLeft.Y + RADIUS +45) 
                Speed.Y *= -0.5F;

            double Angle = Math.Atan2((Speed.Y), (Speed.X));
            if (Angle < 0) 
                Angle += 360;
            Vector2 F = new Vector2(0,0);
            if (Angle >= 0 && Angle <= 90) 
                F = new Vector2((float)(Table.FRICTION * Math.Cos(Angle)), (float)(Table.FRICTION * Math.Sin(Angle)));

            else if ((Angle > 90 && Angle <= 180)||(Angle > 180 && Angle <= 270))
            {
                Angle = Math.Abs(Angle - 180); 
                F = new Vector2((float)(Table.FRICTION * Math.Cos(Angle)), (float)(Table.FRICTION * Math.Sin(Angle)));
            }

            else if (Angle > 270 && Angle <= 360)
            {
                Angle = Math.Abs(Angle - 360);
                F = new Vector2((float)(Table.FRICTION * Math.Cos(Angle)), (float)(Table.FRICTION * Math.Sin(Angle)));
            }

            if (Speed.X>0) Speed.X = Math.Max(Speed.X - F.X, 0);
            else if (Speed.X < 0) Speed.X = Math.Min(Speed.X + F.X, 0);

            if (Speed.Y > 0) Speed.Y = Math.Max(Speed.Y - F.Y, 0);
            else if (Speed.Y < 0) Speed.Y = Math.Min(Speed.Y + F.Y, 0);
        }

    }
}
