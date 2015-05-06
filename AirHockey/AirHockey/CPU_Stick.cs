using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class CPU_Stick : Stick
    {
        Vector2 DefaultPosition;
        public CPU_Stick(NewGame game)
            : base(game)
        {
            Velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            DefaultPosition = Position = new Vector2(game.GameTable.TableTopLeft.X + Table.WIDTH - Table.Thickness - 50 - RADIUS, game.GameTable.TableTopLeft.Y + Table.HEIGHT / 2);
            base.LoadContent();
        }

        public override void Move(GameTime Time)
        {
            //if (Position.X <= game.NewDisc.Position.X + RADIUS + game.NewDisc.RADIUS) 
            //{
            //    if (Velocity.X == Table.WIDTH - Table.Thickness - RADIUS - Position.X)
            //        Velocity = game.NewDisc.Position - Position;
            //    else
            //    Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
            //}
            if (game.NewDisc.Position.X >= Table.WIDTH / 2 + game.NewDisc.RADIUS)
            { 
                AI(); 
            }
            else
            {
                Velocity = (DefaultPosition - Position); 
            }
            Velocity *= 0.3f;
            BoundPositionInTable(this, Velocity * Time.ElapsedGameTime.Milliseconds / 60f);
            Position.X = Math.Max(Position.X, Table.WIDTH / 2 + RADIUS);
        }

        private void AI()
        {
            Vector2 goal = new Vector2(Table.Thickness, Table.HEIGHT / 2);
            double Angle = Math.Atan2(game.NewDisc.Position.Y + game.NewDisc.Velocity.Y - goal.Y, game.NewDisc.Position.X + game.NewDisc.Velocity.X - goal.X) * (180 / Math.PI);
            double Angle1 = Angle - Math.Atan2(Position.Y - goal.Y, Position.X - goal.X) * (180 / Math.PI);
            Angle += 90;
            Angle *= Math.PI / 180;
            double length = new Vector2(Position.X - goal.X, Position.Y - goal.Y).Length() * Math.Sin(Angle1 * (Math.PI / 180));
            Vector2 Desired = new Vector2((float)(length * Math.Cos(Angle)), (float)(length * Math.Sin(Angle)));
            if (Desired.Y + Position.Y > Table.HEIGHT - Table.Thickness - RADIUS)
            {
                Desired.Y = Table.HEIGHT - Table.Thickness - RADIUS;
                Desired.X = Desired.Y * (game.NewDisc.Position.X + game.NewDisc.Velocity.X) / (game.NewDisc.Position.Y + game.NewDisc.Velocity.Y);
            }
            else if (Desired.Y + Position.Y < Table.Thickness + RADIUS)
            {
                Desired.Y = Table.Thickness + RADIUS;
                Desired.X = Desired.Y * (game.NewDisc.Position.X + game.NewDisc.Velocity.X) / (game.NewDisc.Position.Y + game.NewDisc.Velocity.Y);
            }
            Angle = Math.Atan2(game.NewDisc.Velocity.Y, game.NewDisc.Velocity.X) * (180 / Math.PI);
            Angle1 = Math.Atan2(Desired.Y, Desired.X) * (180 / Math.PI);
            Desired.X = (game.NewDisc.Velocity.X - (Position.X - game.NewDisc.Position.X - RADIUS))/3;
            if ((Position-game.NewDisc.Position).Length()<=200&&game.NewDisc.Position.X<Position.X)//(Math.Abs(Angle-Angle1)<=180)
            {
                Velocity = game.NewDisc.Position - Position;
                return;
            }
            Velocity = Desired;
            //if (Position.X <= game.NewDisc.Position.X + RADIUS + game.NewDisc.RADIUS)
            //{
            //    if (Velocity.X == Table.WIDTH - Table.Thickness - RADIUS - Position.X)
            //        Velocity = game.NewDisc.Position - Position;
            //    else
            //        Velocity.X = Table.WIDTH - Table.Thickness - RADIUS - Position.X;
            //}
        }
    }
}
