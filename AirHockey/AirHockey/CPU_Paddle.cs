using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class CPU_Paddle : Paddle
    {
        Vector2 DefaultPosition;
        public CPU_Paddle(NewGame game)
            : base(game)
        {
            Velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            DefaultPosition = new Vector2(Table.WIDTH - 70, Table.HEIGHT / 2);
            base.LoadContent();
        }

        public override void Move(GameTime Time)
        {
            if (Position.X <= game.NewPuck.Position.X)
            {
                Defense();
            }
            else if (game.NewPuck.Position.X >= Table.WIDTH / 2 + game.NewPuck.RADIUS)
            {
                Offense();
            }
            else
            {
                ReturnToDefaultPosition();
            } 

            Velocity *= 0.2f; //Decrease Paddle's Velocity
            BoundPositionInTable(this, Velocity * Time.ElapsedGameTime.Milliseconds / 60f);
            Position.X = Math.Max(Position.X, Table.WIDTH / 2 + RADIUS);
        }

        private void ReturnToDefaultPosition(){
            Velocity = (DefaultPosition - Position)*1.3f;
        }

        private void Offense()
        {
            //Vector2 Pos = Position;
            //Position = game.NewPuck.Position;// +game.NewPuck.Velocity * 2;
            //BoundPositionInTable(this, Vector2.Zero);
            //Position += Pos; Pos = Position - Pos; Position -= Pos;
            //Vector2 goal = new Vector2(Table.Thickness, Table.HEIGHT / 2);
            //Vector2 Desired = Pos - goal;
            //double Angle = Math.Atan2(Desired.Y, Desired.X);
            //float Magnitude = Desired.Length();
            //Magnitude += RADIUS + game.NewPuck.RADIUS;
            //Desired = new Vector2(Magnitude * (float)Math.Cos(Angle), Magnitude * (float)Math.Sin(Angle));
            //Desired += goal;
            //if (Position == Desired || (Position - game.NewPuck.Position).Length() < RADIUS + game.NewPuck.RADIUS + 10)
            //{
            //    Velocity = (game.NewPuck.Position - Position);// *1.3f;
            //}
            //else Velocity = (Desired - Position) * 1.3f;

            //if (game.NewPuck.Velocity.X < 0) Velocity.X = (game.NewPuck.Velocity.X * 2f) * 1.3f;

            Velocity = game.NewPuck.Position - Position;
            
        }

        private void Defense()
        {
            Velocity = (DefaultPosition - Position);
            if (game.NewPuck.Velocity.X > 0)
            {
                Velocity.X = game.NewPuck.Velocity.X * 3.5f;
            }
            else
            {
                Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
            }
            Velocity *= 1.2f;
        }
    }
}
