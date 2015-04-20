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
            Position = new Vector2(Table.WIDTH / 2 , 70);
        }

        public void LoadContent()
        {
            base.LoadContent();
        }

        public override void Movement()
        {
            base.Movement();
            Position.Y = Math.Min(Position.Y, Table.LENGTH / 2 - RADIUS);
        }
    }
}
