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
        Vector2 PreviousMousePostion;
        MouseState MOUSE;
        public Player_Stick(NewGame game) : base(game)
        {
            Velocity = new Vector2(0, 0);
            PreviousMousePostion = new Vector2(MOUSE.X, MOUSE.Y);
        }

        public void LoadContent()
        {
            Mouse.SetPosition(70, Table.HEIGHT / 2);
            Position = new Vector2(70, Table.HEIGHT / 2);
            base.LoadContent();
        }

        override public void Movement()
        {
            MOUSE = Mouse.GetState();//get Mouse Position
            Vector2 CurrentMousePosition = new Vector2(MOUSE.X, MOUSE.Y);
            Velocity = (CurrentMousePosition - PreviousMousePostion);
            PreviousMousePostion = CurrentMousePosition;
            base.Movement();
            Position.X = Math.Min(Position.X, (Table.WIDTH / 2) - RADIUS);//Limit Stick Postion
            base.Hit();
        }

    }
}
