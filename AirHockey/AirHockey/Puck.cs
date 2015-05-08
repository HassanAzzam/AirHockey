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
    public class Puck : GameElement
    {
        private Vector2 Acceleration;
        private Vector2 PreviousPosition;
        private Vector2 ObjVelocity;
        private int MaximumSpeed;
        private float FrictionCoefficient;
        bool CornerCollision;

        public Puck(NewGame Game)
            : base(Game)
        {
            this.Velocity = Vector2.Zero;
            this.PreviousPosition = Vector2.Zero;
            this.FrictionCoefficient = 0.9f;
            this.Acceleration = new Vector2(this.FrictionCoefficient * 9.8f, this.FrictionCoefficient * 9.8f);
            this.MaximumSpeed = 125;
            this.Mass = 0.05f;
            this.CornerCollision = false;
            this.Game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            this.Texture = Game.Content.Load<Texture2D>("Puck");
            this.Radius = this.Texture.Width / 2;
            this.Initialize();
            base.LoadContent();
        }

        public void Draw()
        {
            Game.SpriteBatch.Draw(this.Texture, Table.TopLeft - new Vector2(this.Radius, this.Radius) + this.Position, Color.White);
        }

        public override void Initialize()
        {
            this.Velocity = Vector2.Zero;
            this.Acceleration = Vector2.Zero;
            this.Position = new Vector2(Table.Width / 2, Table.Height / 2);
        }

        public override void Move(GameTime Time)
        {

            #region Goal Checking
            if (Game.GameTable.CheckGoal(Game.NewCPU, this.Position))
            {
                if (!Game.Mute)
                {
                    Game.PuckHitGoal.Play();
                }
                ++Game.NewCPU.Score;
                Game.GoalScored();
                return;
            }
            if (Game.GameTable.CheckGoal(Game.NewPlayer, this.Position))
            {
                if (!Game.Mute)
                {
                    Game.PuckHitGoal.Play();
                }
                ++Game.NewPlayer.Score;
                Game.GoalScored();
                return;
            }
            #endregion

            #region Hitting

            if (this.Intersects(Game.NewPlayer))
            {
                this.Hit(Game.NewPlayer);
            }
            else if (this.Intersects(Game.NewCPU))
            {
                this.Hit(Game.NewCPU);
            }
            else
            {
                if (this.CornerCollision)
                {
                    this.Velocity = this.ObjVelocity * 7f;
                    this.CornerCollision = false;
                }
                this.PreviousPosition = Vector2.Zero;
            }
            #endregion

            double time = Time.ElapsedGameTime.TotalSeconds;
            double Angle = Math.Atan2(Velocity.Y, Velocity.X);
            this.Acceleration = new Vector2(this.FrictionCoefficient * 9.8f, this.FrictionCoefficient * 9.8f);
            this.Acceleration *= (float)time;
            this.Acceleration.X *= (float)Math.Cos(Math.Abs(Angle));
            this.Acceleration.Y *= (float)Math.Sin(Math.Abs(Angle));

            if (this.Velocity.X > 0)
            {
                this.Velocity.X -= this.Acceleration.X;
            }
            else if (this.Velocity.X < 0)
            {
                this.Velocity.X += this.Acceleration.X;
            }
            if (this.Velocity.Y > 0)
            {
                this.Velocity.Y -= this.Acceleration.Y;
            }
            else if (this.Velocity.Y < 0)
            {
                this.Velocity.Y += this.Acceleration.Y;
            }

            this.BoundPositionInTable(this, this.Velocity * Time.ElapsedGameTime.Milliseconds / 60f);

            if (this.Position.X == Table.Width - this.Radius - Table.Thickness)
            {
                if (!Game.Mute)
                {
                    Game.PuckSound.Play();
                }
                this.Velocity.X *= -1;
            }
            if (this.Position.X == this.Radius + Table.Thickness)
            {
                if (!Game.Mute)
                {
                    Game.PuckSound.Play();
                }
                this.Velocity.X *= -1;
            }
            if (this.Position.Y == Table.Height - this.Radius - Table.Thickness)
            {
                if (!Game.Mute)
                {
                    Game.PuckSound.Play();
                }
                this.Velocity.Y *= -1;
            }
            if (this.Position.Y == this.Radius + Table.Thickness)
            {
                if (!Game.Mute)
                {
                    Game.PuckSound.Play();
                }
                this.Velocity.Y *= -1;
            }
            if (this.Velocity.Length() > this.MaximumSpeed)
            {
                this.Velocity = new Vector2((this.Velocity.X / this.Velocity.Length()) * this.MaximumSpeed, (this.Velocity.Y / this.Velocity.Length()) * this.MaximumSpeed);
            }
        }

        private bool Intersects(User obj)
        {
            double Distance;
            Distance = (obj.Radius + this.Radius) - (obj.Position - this.Position).Length();
            return Distance >= 0;
        }

        public void Hit(User UserObj)
        {
            if (!Game.Mute)
            {
                Game.PuckSound.Play();
            }
            double Distance = this.Radius + UserObj.Radius - (UserObj.Position - this.Position).Length();

            if (this.InCorner() && Distance >= 0)
            {
                this.CornerCollision = true;
                this.MintainCollision(UserObj);
                this.Velocity = Vector2.Zero;
                this.ObjVelocity = UserObj.Velocity;
            }
            else if (this.OnSide() && Distance >= 0)
            {
                this.MintainCollision(UserObj);
                this.ChangePuckVelocity(UserObj);
            }
            else
            {
                this.ChangePuckVelocity(UserObj);
            }
            this.PreviousPosition = this.Position;
        }

        bool OnSide()
        {
            return (this.Position.X == Table.Thickness + this.Radius) || ((this.Position.X == Table.Width - Table.Thickness - this.Radius) || (this.Position.Y == Table.Thickness + this.Radius) || (this.Position.Y == Table.Height - Table.Thickness - this.Radius));
        }

        bool InCorner()
        {
            return (this.Position.X == Table.Thickness + this.Radius && this.Position.Y == Table.Thickness + this.Radius) || (this.Position.X == Table.Thickness + this.Radius && this.Position.Y == Table.Height - Table.Thickness - Radius) || (this.Position.X == Table.Width - Table.Thickness - this.Radius && this.Position.Y == Table.Thickness + this.Radius) || (this.Position.X == Table.Width - Table.Thickness - this.Radius && this.Position.Y == Table.Height - Table.Thickness - this.Radius);
        }

        private void MintainCollision(User UserObj)
        {
            double Distance = this.Radius + UserObj.Radius - (UserObj.Position - this.Position).Length();

            if (Distance >= 0)
            {
                double Angle = Math.Atan2(-1 * UserObj.Velocity.Y, -1 * UserObj.Velocity.X);

                UserObj.Position = new Vector2((float)(UserObj.Position.X + Distance * Math.Cos(Angle)), (float)(UserObj.Position.Y + Distance * Math.Sin(Angle)));
            }

            Mouse.SetPosition((int)(Game.NewPlayer.Position.X), (int)(Game.NewPlayer.Position.Y));
        }

        private void ChangePuckVelocity(User UserObj)
        {
            //Get angle of Puck Velocity reflection vector caused by the hit
            double Angle = Math.Atan2((this.Position.Y - UserObj.Position.Y), (this.Position.X - UserObj.Position.X));

            double VelocityMagnitude;

            //Get new velocity magnitude of Puck Velocity according to Momentum Conservation Law
            VelocityMagnitude = (UserObj.Mass * UserObj.Velocity.Length()) / this.Mass + this.Velocity.Length();

            //Velocity resolution
            this.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));
        }
    }
}