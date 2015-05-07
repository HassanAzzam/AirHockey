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
                //if (Velocity.X == Table.WIDTH - Table.Thickness - RADIUS - Position.X)
                //    Velocity = game.NewPuck.Position - Position;
                //else
                //    Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
                Velocity = (DefaultPosition - Position);
                Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
            }
            else if (game.NewPuck.Position.X >= Table.WIDTH / 2 + game.NewPuck.RADIUS)
            {
                AI();
            }
            else
            {
                Velocity.Y = game.NewPuck.Position.Y - Position.Y;
                Velocity.X += 20;
                //(DefaultPosition.X - Position.X);
            }
            Velocity *= 0.2f;
            BoundPositionInTable(this, Velocity * Time.ElapsedGameTime.Milliseconds / 60f);
            Position.X = Math.Max(Position.X, Table.WIDTH / 2 + RADIUS);
        }

        private void AI()
        {
            Vector2 Pos = Position;
            Position = game.NewPuck.Position;// +game.NewPuck.Velocity * 2;
            BoundPositionInTable(this, Vector2.Zero);
            Position += Pos; Pos = Position - Pos; Position -= Pos;
            Vector2 goal = new Vector2(Table.Thickness, Table.HEIGHT / 2);
            Vector2 Desired = Pos - goal;
            double Angle = Math.Atan2(Desired.Y, Desired.X);
            float Magnitude = Desired.Length();
            Magnitude += RADIUS + game.NewPuck.RADIUS;
            Desired = new Vector2(Magnitude * (float)Math.Cos(Angle), Magnitude * (float)Math.Sin(Angle));
            Desired += goal;
            if (Position == Desired || (Position - game.NewPuck.Position).Length() < RADIUS + game.NewPuck.RADIUS + 10)
            {
                Velocity = (game.NewPuck.Position - Position) * 1.3f;
            }
            else Velocity = (Desired - Position) * 1.3f;

            if (game.NewPuck.Velocity.X < 0) Velocity.X = (game.NewPuck.Velocity.X * 2f) * 1.3f;
            //double Angle = Math.Atan2(game.NewPuck.Position.Y + game.NewPuck.Velocity.Y - goal.Y, game.NewPuck.Position.X + game.NewPuck.Velocity.X - goal.X) * (180 / Math.PI);
            //double Angle1 = Angle - Math.Atan2(Position.Y - goal.Y, Position.X - goal.X) * (180 / Math.PI);
            //Angle += 90;
            //Angle *= Math.PI / 180;
            //double length = new Vector2(Position.X - goal.X, Position.Y - goal.Y).Length() * Math.Sin(Angle1 * (Math.PI / 180));
            //Vector2 Desired = new Vector2((float)(length * Math.Cos(Angle)), (float)(length * Math.Sin(Angle)));
            //if (Desired.Y + Position.Y > Table.HEIGHT - Table.Thickness - RADIUS)
            //{
            //    Desired.Y = Table.HEIGHT - Table.Thickness - RADIUS;
            //    Desired.X = Desired.Y * (game.NewPuck.Position.X + game.NewPuck.Velocity.X) / (game.NewPuck.Position.Y + game.NewPuck.Velocity.Y);
            //}
            //else if (Desired.Y + Position.Y < Table.Thickness + RADIUS)
            //{
            //    Desired.Y = Table.Thickness + RADIUS;
            //    Desired.X = Desired.Y * (game.NewPuck.Position.X + game.NewPuck.Velocity.X) / (game.NewPuck.Position.Y + game.NewPuck.Velocity.Y);
            //}
            //Angle = Math.Atan2(game.NewPuck.Velocity.Y, game.NewPuck.Velocity.X) * (180 / Math.PI);
            //Angle1 = Math.Atan2(Desired.Y, Desired.X) * (180 / Math.PI);
            //Desired.X = (game.NewPuck.Velocity.X - (Position.X - game.NewPuck.Position.X - RADIUS))/3;
            //if ((Position-game.NewPuck.Position).Length()<=200&&game.NewPuck.Position.X<Position.X)//(Math.Abs(Angle-Angle1)<=180)
            //{
            //    Velocity = game.NewPuck.Position - Position;
            //    return;
            //}
            //Velocity = Desired;
            //if (Position.X <= game.NewPuck.Position.X + RADIUS + game.NewPuck.RADIUS)
            //{
            //    if (Velocity.X == Table.WIDTH - Table.Thickness - RADIUS - Position.X)
            //        Velocity = game.NewPuck.Position - Position;
            //    else
            //        Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
            //}
        }
    }
}
