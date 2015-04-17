using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class Player_Stick : Stick
    {
        public Player_Stick(NewGame game) : base(game)
        {
            Position = new Vector2(Table.WIDTH / 2 - (float)19.5, Table.LENGTH - 70 - 39);
        }

        public void LoadContent()
        {
            base.LoadContent();
        }

        override public void Movement()
        {
           
        }

    }
}
