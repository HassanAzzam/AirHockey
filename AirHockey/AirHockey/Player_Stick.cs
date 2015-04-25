using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
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


namespace AirHockey
{
    public class Player_Stick : Stick
    {
        public Player_Stick(NewGame game) : base(game)
        {
            Velocity = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            Position = new Vector2(70, Table.HEIGHT / 2);
            base.LoadContent();
        }

        override public void Movement()
        {
            Position.X = Math.Min(Position.X, (Table.WIDTH / 2) - RADIUS);
            base.Movement();
        }

    }
}
