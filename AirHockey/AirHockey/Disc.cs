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
using System.Threading;

namespace AirHockey
{
    public class Disc : GameElement
    {
        private float FrictionCoefficient = 0.5f;
        private Vector2 Acceleration; //Alaa: Puck mass is around 50 grams
        public Disc(NewGame game)
            : base(game)
        {
            Mass = 0.05F;
            game.Content.RootDirectory = "Content";
            Velocity = Vector2.Zero;

            //Velocity = new Vector2(10,0);
            Acceleration = new Vector2(FrictionCoefficient * 9.8f, FrictionCoefficient * 9.8f);
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Disc");
            RADIUS = TEXTURE.Width / 2;
            Initialize();
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft - new Vector2(RADIUS, RADIUS) + Position, Color.White);
        }

        public override void Initialize()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Position = new Vector2(Table.WIDTH / 2, Table.HEIGHT / 2);
        }
        
        public override void Move(GameTime Time)
        {
            #region Goal Checking

            if (game.GameTable.CheckGoal(game.NewCPU, Position))
            {
                ++game.NewCPU.Points;
                game.GoalScored();
                return;
            }
            else if (game.GameTable.CheckGoal(game.NewPlayer, Position))
            {
                ++game.NewPlayer.Points;
                game.GoalScored();
                return;
            }

            #endregion
            
            #region Hitting

            if (this.Intersects(game.NewPlayer))
            {
                Hit(game.NewPlayer);
            }
            if (this.Intersects(game.NewCPU))
            {
                Hit(game.NewCPU);
            }

            #endregion

           // Velocity = Vector2.Zero;
            Acceleration = new Vector2(FrictionCoefficient * 9.8f, FrictionCoefficient * 9.8f);
            double time = Time.ElapsedGameTime.TotalSeconds;
            Acceleration *= (float)time;
            double Angle = Math.Atan2(Velocity.Y , Velocity.X);
            Acceleration.X *= (float)Math.Cos(Math.Abs(Angle));
            Acceleration.Y *= (float)Math.Sin(Math.Abs(Angle));
            
            if (Velocity.X > 0)
            {
                Velocity.X = Math.Max(Velocity.X - Acceleration.X, 0);
            }
            else if (Velocity.X < 0)
            {

                Velocity.X = Math.Min(Velocity.X + Acceleration.X, 0);
            }
            if (Velocity.Y > 0)
            {

                Velocity.Y = Math.Max(Velocity.Y -  Acceleration.Y, 0);
            }
            else if (Velocity.Y < 0)
            {

                Velocity.Y = Math.Min(Velocity.Y + Acceleration.Y, 0);
            }

            //Moving The Puck
            Velocity *= 0.995f; //Multiplied to reduce the puck speed
            
            BoundPositionInTable(this, Velocity * Time.ElapsedGameTime.Milliseconds / 60f);

            if (Position.X == Table.WIDTH - RADIUS - Table.Thickness)
            {
                Velocity.X *= -1;
            }
            if (Position.X == RADIUS + Table.Thickness)
            {
                Velocity.X *= -1;
            }
            if (Position.Y == Table.HEIGHT - RADIUS - Table.Thickness)
            {
                Velocity.Y *= -1;
            }
            if (Position.Y == RADIUS + Table.Thickness)
            {
                Velocity.Y *= -1;
            }
        }

        private bool Intersects(User obj)
        {
            double Distance;
            Distance = (obj.RADIUS + RADIUS) - (obj.Position - Position).Length();
            return Distance > 0;
        }

        public void Hit(User UserObj)
        {
            double Distance = (UserObj.RADIUS + RADIUS) - (UserObj.Position - Position).Length();

           // MintainCollision(UserObj);

            ChangeDiscVelocity(UserObj);
        }

        private void MintainCollision(User UserObj)
        {
            
            Vector2 DISC_POSITION = this.Position;
            Vector2 IntersectingPoint;
            double Distance;
            double StickVelocityMagnitude = UserObj.Velocity.Length();
            double DiscVelocityMagnitude = this.Velocity.Length();

            //Vector from Disc Position to Stick Center
            Vector2 STICK_CENTER = UserObj.Position - this.Position;
            double StickCenterMagnitude = STICK_CENTER.Length();

            double RadiusSum = UserObj.RADIUS + this.RADIUS;
            double Length_AngleRatio;

            //Get angle of Stick Velocity vector
            double Angle_StickVelocity = Math.Atan2(UserObj.Velocity.Y, UserObj.Velocity.X) * (180 / Math.PI);
            while (Angle_StickVelocity < 0) Angle_StickVelocity += 360;

            //Get angle of Disc Velocity vector
            double Angle_DiscVelocity = Math.Atan2(this.Velocity.Y, this.Velocity.X) * (180 / Math.PI);
            while (Angle_DiscVelocity < 0) Angle_DiscVelocity += 360;

            //Get angle of StickCenter Vector
            double Angle_StickCenter = Math.Atan2(STICK_CENTER.Y, STICK_CENTER.X) * (180 / Math.PI);

            //Determine which direction the disc will move in
            if (Math.Abs(Angle_DiscVelocity - Angle_StickVelocity) >= 90)
            {
                Angle_DiscVelocity = Math.Atan2(-1 * this.Velocity.Y, -1 * this.Velocity.X) * (180 / Math.PI);
            }
            else if (this.Velocity == Vector2.Zero)
            {
                Angle_DiscVelocity = Angle_StickCenter + 180;
            }
            else
            {
                Angle_DiscVelocity -= 360;
            }

            //Get relation between Triangle Lengths and Sin of Angles
            Length_AngleRatio = RadiusSum / Math.Sin((Angle_StickCenter - Angle_DiscVelocity) * (Math.PI / 180));

            //Get Desired Point Angle
            double Angle_IntersectingPoint = (StickCenterMagnitude / Length_AngleRatio);
            Angle_IntersectingPoint = Math.Asin(Angle_IntersectingPoint) * (180 / Math.PI);

            //Get Distance between Disc Position and desired point
            Distance = Length_AngleRatio * Math.Sin((180 - (Angle_StickCenter - Angle_DiscVelocity) - Angle_IntersectingPoint) * Math.PI / 180);

            //Get Desired Point
            IntersectingPoint = new Vector2((float)(this.Position.X + Distance * Math.Cos(Angle_DiscVelocity * (Math.PI / 180))), (float)(this.Position.Y + Distance * Math.Sin(Angle_DiscVelocity * (Math.PI / 180))));

            //Return Disc to the Intersecting Point before collision -Intersection between Disc and Stick is one point only-
            this.Position = IntersectingPoint;
            BoundPositionInTable(this, Vector2.Zero);

            Distance = RadiusSum - (this.Position - UserObj.Position).Length();
            if (Distance > 0) //if Collision stills, Move Stick instead
            {
                double Angle = Math.Atan2(-1 * UserObj.Velocity.Y, -1 * UserObj.Velocity.X);

                Position = new Vector2((float)(UserObj.Position.X + Distance * Math.Cos(Angle)), (float)(UserObj.Position.Y + Distance * Math.Sin(Angle)));
            }
        }

        private void ChangeDiscVelocity(User UserObj)
        {
            //Get angle of Disc Velocity reflection vector caused by the hit
            double Angle = Math.Atan2((this.Position.Y - UserObj.Position.Y), (this.Position.X - UserObj.Position.X));

            //According to Momentum Conservation Law
            double VelocityMagnitude;

            if (UserObj.Velocity != Vector2.Zero) //If Stick is Moving
            {
                //Get Velocity magnitude of Stick Velocity
                VelocityMagnitude = (UserObj.Mass * UserObj.Velocity.Length()) / Mass + Velocity.Length();
            }
            else
            {
                //Get Velocity magnitude of Disc Velocity
                VelocityMagnitude = Velocity.Length();
            }

            //Velocity resolution
            this.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));

        }
    }
}
