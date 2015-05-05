﻿using System;
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
            if (game.GameTable.CheckGoal(game.NewCPU, Position))
            {
                game.HitGoal.Play();
                ++game.NewCPU.Points;
                game.GoalScored();
            }
            else if (game.GameTable.CheckGoal(game.NewPlayer, Position))
            {
                game.HitGoal.Play();
                ++game.NewPlayer.Points;
                game.GoalScored();
            }

            if (this.Intersects(game.NewPlayer))
            {
                Hit(game.NewPlayer);
            }
            if (this.Intersects(game.NewCPU))
            {
                Hit(game.NewCPU);
            }
            Acceleration = new Vector2(FrictionCoefficient * 9.8f, FrictionCoefficient * 9.8f);
            double time = Time.ElapsedGameTime.TotalSeconds;
            Acceleration *= (float)time;
            double Angle = Math.Atan(Velocity.Y / Velocity.X);
            Acceleration.X *= (float)Math.Cos(Math.Abs(Angle));
            Acceleration.Y *= (float)Math.Sin(Math.Abs(Angle));
            
            if (Velocity.X > 0)
            {
                Velocity.X -= Acceleration.X;
            }
            else if (Velocity.X < 0)
            {

                Velocity.X += Acceleration.X;
            }
            if (Velocity.Y > 0)
            {

                Velocity.Y -= Acceleration.Y;
            }
            else if (Velocity.Y < 0)
            {

                Velocity.Y += Acceleration.Y;
            }

            //Moving The Puck
            //Position += Velocity * 0.008f; //Multiplied to reduce the puck speed

            //test ya alaa :D
            #region Limit Puck Speed
            if (Velocity.X > 30)
                Velocity.X = 30;
            if (Velocity.Y > 30)
                Velocity.Y = 30;
            if (Velocity.X < -30)
                Velocity.X = -30;
            if (Velocity.Y < -30)
                Velocity.Y = -30;
            #endregion

            Vector2 V = Velocity;
            V *= (float)(Time.ElapsedGameTime.Milliseconds / 16.666667);
            BoundPositionInTable(this, V);

            if (Position.X == Table.WIDTH - RADIUS - 20)
            {
                game.HitBorders.Play();
                Velocity.X *= -1;
            }
            if (Position.X == RADIUS + 20)
            {
                game.HitBorders.Play();
                Velocity.X *= -1;
            }
            if (Position.Y == Table.HEIGHT - RADIUS - 20)
            {
                game.HitBorders.Play();
                Velocity.Y *= -1;
            }
            if (Position.Y == RADIUS + 20)
            {
                game.HitBorders.Play();
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
            game.StickHitPuck.Play();
            double Distance = (UserObj.RADIUS + RADIUS) - (UserObj.Position - Position).Length();

            MintainCollision(UserObj);

            ChangeDiscVelocity(UserObj);
        }

        private void MintainCollision(User UserObj)
        {
            
            Vector2 DISC_POSITION = this.Position;
            Vector2 IntersectingPoint;
            double Distance;
            double StickVelocityMagnitude = UserObj.Velocity.X * UserObj.Velocity.X + UserObj.Velocity.Y * UserObj.Velocity.Y; StickVelocityMagnitude = Math.Sqrt(StickVelocityMagnitude);
            double DiscVelocityMagnitude = this.Velocity.X * this.Velocity.X + this.Velocity.Y * this.Velocity.Y; DiscVelocityMagnitude = Math.Sqrt(DiscVelocityMagnitude);

            //Vector from Disc Position to Stick Center
            Vector2 STICK_CENTER = UserObj.Position - this.Position;
            double StickCenterMagnitude = STICK_CENTER.X * STICK_CENTER.X + STICK_CENTER.Y * STICK_CENTER.Y;
            StickCenterMagnitude = Math.Sqrt(StickCenterMagnitude);

            double RadiusSum = UserObj.RADIUS + this.RADIUS;
            double Length_AngleRatio;

            //Get angle of Stick Velocity vector
            double Angle_StickVelocity = Math.Atan2(UserObj.Velocity.Y, UserObj.Velocity.X) * (180 / Math.PI);
            while (Angle_StickVelocity < 0) Angle_StickVelocity += 360;

            //Get angle of Disc Velocity vector
            double Angle_DiscVelocity = Math.Atan2(this.Velocity.Y, this.Velocity.X) * (180 / Math.PI);
            while (Angle_DiscVelocity < 0) Angle_DiscVelocity += 360;

            //Determine which direction the disc will move in
            if (Math.Abs(Angle_DiscVelocity - Angle_StickVelocity) >= 90 || StickVelocityMagnitude <= DiscVelocityMagnitude)
            {
                Angle_DiscVelocity = Math.Atan2(-1 * this.Velocity.Y, -1 * this.Velocity.X) * (180 / Math.PI);
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
            IntersectingPoint = new Vector2((float)(this.Position.X + Distance * Math.Cos(Angle_DiscVelocity * (Math.PI / 180))), (float)(this.Position.Y + Distance * Math.Sin(Angle_DiscVelocity * (Math.PI / 180))));

            //Return Disc to the Intersecting Point before collision -Intersection between Disc and Stick is one point only-
            this.Position = IntersectingPoint;
            BoundPositionInTable(this, Vector2.Zero);

            Distance = (this.RADIUS + UserObj.RADIUS) - (this.Position - UserObj.Position).Length();
            if (Distance > 0) //if Collision stills, Move Stick instead
            {
                double Angle = Math.Atan2(-1 * UserObj.Velocity.Y, -1 * UserObj.Velocity.X);

                Position = new Vector2((float)(UserObj.Position.X + Distance * Math.Cos(Angle)), (float)(UserObj.Position.Y + Distance * Math.Sin(Angle)));
            }
        }

        private void ChangeDiscVelocity(User UserObj)
        {
            ////Get angle of Disc Velocity reflection vector caused by the hit
            //double Angle = Math.Atan2((this.Position.Y - STICK.Position.Y), (this.Position.X - STICK.Position.X));

            //double VelocityMagnitude;

            //if (Velocity != new Vector2(0, 0)) //If Stick is Moving
            //{
            //    //Get Velocity magnitude of Stick Velocity
            //    VelocityMagnitude = STICK.Velocity.X * STICK.Velocity.X + STICK.Velocity.Y * STICK.Velocity.Y;
            //    VelocityMagnitude = Math.Sqrt(VelocityMagnitude);

            //    //Velocity resolution
            //    this.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
            //}
            //else
            //{
            //    //Get Velocity magnitude of Disc Velocity
            //    VelocityMagnitude = this.Velocity.X * this.Velocity.X + this.Velocity.Y * this.Velocity.Y;
            //    VelocityMagnitude = Math.Sqrt(VelocityMagnitude);
            //    this.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
            //}


            ////////////////////////

            //According to Momentum Conservation Law
            Velocity = (UserObj.Mass * UserObj.Velocity) / Mass + Velocity;
            //If the Stick isn't moving the puck changes its direction
            if (UserObj.Velocity == Vector2.Zero)
                Velocity *= -1;
        }
    }
}
