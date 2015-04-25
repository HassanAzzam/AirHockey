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
            Position = new Vector2(Table.WIDTH / 2, Table.LENGTH - 70 - 39);
            Velocity = new Vector2(0, 0);
           
        }

        public void LoadContent()
        {
            base.LoadContent();
        }

        override public void Movement()
        {
            MouseState MOUSE = Mouse.GetState();
            Vector2 NextPos = new Vector2(MOUSE.X,MOUSE.Y);
            Velocity = NextPos - Position;
            base.Movement();
            Position.Y = Math.Max(Position.Y,Table.LENGTH/2 + RADIUS);
            base.Hit();
        }

    }
}
