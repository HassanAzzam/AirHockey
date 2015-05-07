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
        private float FrictionCoefficient = 0.7f;//Alaa: Friction Coefficient is 0.12 :D
        private int MAX_SPEED=200;//Alaa: Puck's MaxSpeed, Modified from here ONLY! 
        private Vector2 Acceleration;
        private Vector2 PreviousPosition;
        bool CornerCollision=false;
        Vector2 ObjVelocity;

        public Puck(NewGame game)
            : base(game)
        {
            Mass = 0.05F; //Alaa: Puck mass is around 50 grams
            game.Content.RootDirectory = "Content";
            Velocity = Vector2.Zero;
            PreviousPosition = Vector2.Zero;
            Acceleration = new Vector2(FrictionCoefficient * 9.8f, FrictionCoefficient * 9.8f);
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Puck");
            RADIUS = TEXTURE.Width / 2;
            Initialize();
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, Table.TopLeft - new Vector2(RADIUS, RADIUS) + Position, Color.White);
        }

        public override void Initialize()
        {
            Velocity = Vector2.Zero;
            Acceleration = Vector2.Zero;
            Position = new Vector2(Table.WIDTH/2, Table.HEIGHT / 2);
        }
        
        public override void Move(GameTime Time)
        {

            #region Goal Checking

            if (game.GameTable.CheckGoal(game.NewCPU, Position))
            {
                ++game.NewCPU.Score;
                game.GoalScored();
                return;
            }
            else if (game.GameTable.CheckGoal(game.NewPlayer, Position))
            {
                ++game.NewPlayer.Score;
                game.GoalScored();
                return;
            }

            #endregion
            
            #region Hitting

            if (this.Intersects(game.NewPlayer))
            {
                Hit(game.NewPlayer);
            }
            else if (this.Intersects(game.NewCPU))
            {
                Hit(game.NewCPU);
            }
            else
            {
                if (CornerCollision)
                {
                    Velocity = ObjVelocity * 7f;
                    CornerCollision = false;
                }
                PreviousPosition = Vector2.Zero;
            }
            #endregion

            Acceleration = new Vector2(FrictionCoefficient * 9.8f, FrictionCoefficient * 9.8f);
            double time = Time.ElapsedGameTime.TotalSeconds;
            Acceleration *= (float)time;
            double Angle = Math.Atan2(Velocity.Y , Velocity.X);
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
            //Alaa: Restricts the Velocity in this range, Permanent solution /O/ ~O~ ~O~ \O\
            if (Velocity.Length() > MAX_SPEED)
            {
                Velocity = new Vector2((Velocity.X / Velocity.Length()) * MAX_SPEED, (Velocity.Y / Velocity.Length()) * MAX_SPEED);
            }
        }

        private bool Intersects(User obj)
        {
            double Distance;
            Distance = (obj.RADIUS + RADIUS) - (obj.Position - Position).Length();
            return Distance >= 0;
        }

        public void Hit(User UserObj)
        {
            double Distance = RADIUS + UserObj.RADIUS - (UserObj.Position - Position).Length();

            if (this.InCorner()&&Distance>=0)
            {
                CornerCollision = true;
                MintainCollision(UserObj);
                Velocity = Vector2.Zero;
                ObjVelocity = UserObj.Velocity;
                
            }
            else if(this.OnSide()&&Distance>=0)
            {
                MintainCollision(UserObj);
                ChangePuckVelocity(UserObj);
            }
            else
            {
                ChangePuckVelocity(UserObj);
            }
            PreviousPosition = Position;
        }

        bool OnSide()
        {
            return (Position.X==Table.Thickness+RADIUS)||((Position.X == Table.WIDTH - Table.Thickness - RADIUS)||(Position.Y == Table.Thickness + RADIUS)||(Position.Y == Table.HEIGHT - Table.Thickness - RADIUS));
        }

        bool InCorner()
        {
            return (Position.X == Table.Thickness + RADIUS && Position.Y == Table.Thickness + RADIUS) || (Position.X == Table.Thickness + RADIUS && Position.Y == Table.HEIGHT - Table.Thickness - RADIUS) || (Position.X == Table.WIDTH - Table.Thickness - RADIUS && Position.Y == Table.Thickness + RADIUS) || (Position.X == Table.WIDTH - Table.Thickness - RADIUS && Position.Y == Table.HEIGHT - Table.Thickness - RADIUS);
        }

        private void MintainCollision(User UserObj)
        {
            //Velocity = Vector2.Zero;
            double Distance = RADIUS + UserObj.RADIUS - (UserObj.Position - Position).Length();

            if (Distance >= 0)
            {
                double Angle = Math.Atan2(-1 * UserObj.Velocity.Y, -1 * UserObj.Velocity.X);

                UserObj.Position = new Vector2((float)(UserObj.Position.X + Distance * Math.Cos(Angle)), (float)(UserObj.Position.Y + Distance * Math.Sin(Angle)));
            }

            Mouse.SetPosition((int)(game.NewPlayer.Position.X), (int)(game.NewPlayer.Position.Y));
        }

        private void ChangePuckVelocity(User UserObj)
        {

            //Get angle of Puck Velocity reflection vector caused by the hit
            double Angle = Math.Atan2((this.Position.Y - UserObj.Position.Y), (this.Position.X - UserObj.Position.X));

            double VelocityMagnitude;

            //Get new velocity magnitude of Puck Velocity according to Momentum Conservation Law
            VelocityMagnitude = (UserObj.Mass * UserObj.Velocity.Length()) / Mass + Velocity.Length();

            //Velocity resolution
            this.Velocity = new Vector2((float)(VelocityMagnitude * Math.Cos(Angle)), (float)(VelocityMagnitude * Math.Sin(Angle)));

        }
    }
}
