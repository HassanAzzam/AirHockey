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
using System.Diagnostics;


namespace AirHockey
{
    public class Player_Paddle : Paddle
    {
        MouseState MOUSE;
        public Player_Paddle(NewGame game)
            : base(game)
        {
            Velocity = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            Mouse.SetPosition((int)(Position.X + Table.TopLeft.X), (int)(Position.Y + Table.TopLeft.Y));
            base.LoadContent();
        }

        override public void Move(GameTime Time)
        {
            MOUSE = Mouse.GetState();//get Mouse Position
            Vector2 PreviousPosition = Position;
            Position = new Vector2(MOUSE.X, MOUSE.Y);
            BoundPositionInTable(this, Vector2.Zero);
            Position.X = Math.Min(Position.X, (Table.WIDTH / 2) - RADIUS);//Limit Paddle Postion
            Velocity = Position - PreviousPosition;
            Mouse.SetPosition((int)Position.X, (int)Position.Y);// Put Cursor on the paddle
        }
    }
}