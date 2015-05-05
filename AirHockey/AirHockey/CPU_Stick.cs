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
            if (Position.X <= game.NewDisc.Position.X) { Velocity.X += RADIUS; }
            else if (game.NewDisc.Position.X >= Table.WIDTH / 2 + game.NewDisc.RADIUS)
            { Velocity = (game.NewDisc.Position - Position) * 0.085f; }
            else { Velocity = (DefaultPosition - Position) * 0.085f; }
            BoundPositionInTable(this, Velocity * Time.ElapsedGameTime.Milliseconds / 60f);
            Position.X = Math.Max(Position.X, Table.WIDTH / 2 + RADIUS);
        }
    }
}
