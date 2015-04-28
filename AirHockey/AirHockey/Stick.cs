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

            MintainCollision(ref DISC);

            ChangeDiscVelocity(ref DISC);
        }

        private void MintainCollision(ref Disc DISC)
        {

            Vector2 DISC_POSITION = DISC.Position;
            Vector2 IntersectingPoint;
            double Distance;
            double StickVelocityMagnitude = Velocity.X * Velocity.X + Velocity.Y * Velocity.Y; StickVelocityMagnitude = Math.Sqrt(StickVelocityMagnitude);
            double DiscVelocityMagnitude = DISC.Velocity.X * DISC.Velocity.X + DISC.Velocity.Y * DISC.Velocity.Y; DiscVelocityMagnitude = Math.Sqrt(DiscVelocityMagnitude);
           
            //Vector from Disc Position to Stick Center
            Vector2 STICK_CENTER = Position - DISC.Position;
            double StickCenterMagnitude = STICK_CENTER.X * STICK_CENTER.X + STICK_CENTER.Y * STICK_CENTER.Y;
            StickCenterMagnitude = Math.Sqrt(StickCenterMagnitude);

            double RadiusSum = RADIUS + DISC.RADIUS;
            double Length_AngleRatio;

            //Get angle of Stick Velocity vector
            double Angle_StickVelocity = Math.Atan2(Velocity.Y, Velocity.X) * (180 / Math.PI);
            while (Angle_StickVelocity < 0) Angle_StickVelocity += 360;

            //Get angle of Disc Velocity vector
            double Angle_DiscVelocity = Math.Atan2(DISC.Velocity.Y, DISC.Velocity.X) * (180 / Math.PI);
            while (Angle_DiscVelocity < 0) Angle_DiscVelocity += 360;

            //Determine which direction the disc will move in
            if (Math.Abs(Angle_DiscVelocity-Angle_StickVelocity)>=90||StickVelocityMagnitude<=DiscVelocityMagnitude)
            {
                Angle_DiscVelocity = Math.Atan2(-1 * DISC.Velocity.Y, -1 * DISC.Velocity.X) * (180 / Math.PI);  
            }
            else
            {
                Angle_DiscVelocity -= 360;
            }

            //Get angle of StickCenter Vector
            double Angle_StickCenter = Math.Atan2(STICK_CENTER.Y, STICK_CENTER.X) * (180 / Math.PI);

            //Get relation between Triangle Lengths and Sin of Angles
            Length_AngleRatio = RadiusSum / Math.Sin((Angle_StickCenter - Angle_DiscVelocity) * (Math.PI / 180));

            //Get Desired Point Angle
            double Angle_IntersectingPoint = (StickCenterMagnitude / Length_AngleRatio);
            Angle_IntersectingPoint = Math.Asin(Angle_IntersectingPoint) * (180 / Math.PI);

            //Get Distance between Disc Position and desired point
            Distance = Length_AngleRatio * Math.Sin((180 - (Angle_StickCenter - Angle_DiscVelocity) - Angle_IntersectingPoint) * Math.PI / 180);

            //Get Desired Point
            IntersectingPoint = new Vector2((float)(DISC.Position.X + Distance * Math.Cos(Angle_DiscVelocity * (Math.PI / 180))), (float)(DISC.Position.Y + Distance * Math.Sin(Angle_DiscVelocity * (Math.PI / 180))));

            //Return Disc to the Intersecting Point before collision -Intersection between Disc and Stick is one point only-
            DISC.Position = IntersectingPoint;
            BoundPositionInTable(DISC, new Vector2(0, 0));

            Distance = (DISC.RADIUS + RADIUS) - (DISC.Position - Position).Length();
            if (Distance>0) //if Collision stills, Move Stick instead
            {
                double Angle = Math.Atan2(-1 * Velocity.Y, -1 * Velocity.X);
                
                Position = new Vector2((float)(Position.X + Distance * Math.Cos(Angle)), (float)(Position.Y + Distance * Math.Sin(Angle)));
            }
        }

        private void ChangeDiscVelocity(ref Disc DISC)
        {
            //Get angle of Disc Velocity reflection vector caused by the hit
            double Angle = Math.Atan2((DISC.Position.Y - Position.Y), (DISC.Position.X - Position.X));

            double VelocityMagnitude;

            if (Velocity != new Vector2(0, 0)) //If Stick is Moving
            {
                //Get Velocity magnitude of Stick Velocity
                VelocityMagnitude = Velocity.X * Velocity.X + Velocity.Y * Velocity.Y;
                VelocityMagnitude = Math.Sqrt(VelocityMagnitude);

                //Velocity resolution
                DISC.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
            }
            else
            {
                //Get Velocity magnitude of Disc Velocity
                VelocityMagnitude = DISC.Velocity.X * DISC.Velocity.X + DISC.Velocity.Y * DISC.Velocity.Y;
                VelocityMagnitude = Math.Sqrt(VelocityMagnitude);

                //reverse Velocity
                DISC.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
            }
        }
    }
}
