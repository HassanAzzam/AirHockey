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
        public CPU_Stick(NewGame game)  : base(game)
        {

        }

        public void LoadContent()
        {
            Position = new Vector2(game.GameTable.TableTopLeft.X + Table.WIDTH - 70 - RADIUS, game.GameTable.TableTopLeft.Y + Table.HEIGHT / 2);
            base.LoadContent();
        }

        public override void Movement()
        {
            base.Movement();
            //Position.Y = Math.Min(Position.Y, Table.HEIGHT / 2 - RADIUS);
        }
    }
}
