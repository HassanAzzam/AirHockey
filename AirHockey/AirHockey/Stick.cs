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
    public abstract class Stick : GameElement
    {
        public Stick(NewGame game)
            : base(game)
        {
            Position = new Vector2(0, 0);
            game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Stick");
            RADIUS = TEXTURE.Width / 2;
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft + this.Position - new Vector2(RADIUS, RADIUS), Color.White);
        }

        protected bool Intersects(ref Disc DISC)
        {
            double Distance = (DISC.RADIUS + RADIUS) - (DISC.Position - Position).Length();
            return Distance > 0;
        }

        public void Hit(ref Disc DISC)
        {
            double Distance = (DISC.RADIUS + RADIUS) - (DISC.Position - Position).Length();
            //Get angle of Disc Velocity vector
            double Angle = Math.Atan2((-1 * DISC.Velocity.Y), (-1 * DISC.Velocity.X));

            //Return Disc to the last possible position before collision -Intersection between Disc and Stick is one point only-
            DISC.Position = new Vector2((float)(DISC.Position.X + Distance * Math.Cos(Angle)), (float)(DISC.Position.Y + Distance * Math.Sin(Angle)));

            //Get angle of Disc Velocity reflection vector caused by the hit
            Angle = Math.Atan2((DISC.Position.Y - Position.Y), (DISC.Position.X - Position.X));

            //Get Velocity magnitude of Stick Velocity
            double VelocityMagnitude = Velocity.X * Velocity.X + Velocity.Y * Velocity.Y;
            VelocityMagnitude = Math.Sqrt(VelocityMagnitude);


            if (Velocity != new Vector2(0, 0)) //If Stick is Moving
            {
                //Velocity resolution
                DISC.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
            }
            else
            {
                //reverse Velocity
                DISC.Velocity = new Vector2((float)(-1 * Math.Cos(Angle)), (float)(-1 * Math.Sin(Angle)));
            }
        }
    }
}
