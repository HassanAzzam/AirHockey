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
        public Stick(NewGame game)  : base(game)
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

        public void Hit()
        {
            double Distance = (game.NewDisc.RADIUS + RADIUS) - (game.NewDisc.Position - Position).Length();
            if (Distance > 0) //On Collision
            {
                //Get angle of Velocity vector
                double Angle = Math.Atan2((-1 * Velocity.Y), (-1 * Velocity.X)); 

                //Return Disc to the last possible position before collision -Intersection between Disc and Stick is one point only-
                Position = new Vector2((float)(Position.X + Distance * Math.Cos(Angle)), (float)(Position.Y + Distance * Math.Sin(Angle)));

                //Get angle of Disc Velocity reflection vector caused by the hit
                Angle = Math.Atan2(( - Position.Y + game.NewDisc.Position.Y), (- Position.X + game.NewDisc.Position.X));

                //Get Velocity magnitude of Stick Velocity
                double VelocityMagnitude = Velocity.X * Velocity.X + Velocity.Y * Velocity.Y;
                VelocityMagnitude = Math.Sqrt(VelocityMagnitude);


                if (Velocity != new Vector2(0, 0)) //If Stick is Moving
                {
                    //Velocity resolution
                    game.NewDisc.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
                }
                else 
                { 
                    //reverse Velocity
                    game.NewDisc.Velocity = new Vector2((float)(-1 * Math.Cos(Angle)), (float)(-1 * Math.Sin(Angle))); 
                }
            }
        }
    }
}
