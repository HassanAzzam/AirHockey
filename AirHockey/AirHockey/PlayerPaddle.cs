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
    public class PlayerPaddle : Paddle
    {
        private MouseState MouseState;
        public PlayerPaddle(NewGame Game)
            : base(Game)
        {
            this.Velocity = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            Mouse.SetPosition((int)(this.Position.X + Table.TopLeft.X), (int)(this.Position.Y + Table.TopLeft.Y));
            base.LoadContent();
        }

        override public void Move(GameTime Time)
        {
            Vector2 PreviousPosition = this.Position;
            this.MouseState = Mouse.GetState();//get Mouse Position
            this.Position = new Vector2(this.MouseState.X, this.MouseState.Y);
            this.BoundPositionInTable(this, Vector2.Zero);
            this.Position.X = Math.Min(this.Position.X, (Table.Width / 2) - this.Radius);//Limit Paddle Postion
            this.Velocity = this.Position - PreviousPosition;
            Mouse.SetPosition((int)this.Position.X, (int)this.Position.Y);// Put Cursor on the paddle
        }
    }
}