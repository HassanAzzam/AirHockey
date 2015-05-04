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
        public CPU_Stick(NewGame game) : base(game)
        {
            Velocity = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            Position = new Vector2(game.GameTable.TableTopLeft.X + Table.WIDTH - 70 - RADIUS, game.GameTable.TableTopLeft.Y + Table.HEIGHT / 2);
            base.LoadContent();
        }

        public override void Move(GameTime Time)
        {
            BoundPositionInTable(this, Velocity);
            
            //Position.Y = Math.Min(Position.Y, Table.HEIGHT / 2 - RADIUS);
        }
    }
}
