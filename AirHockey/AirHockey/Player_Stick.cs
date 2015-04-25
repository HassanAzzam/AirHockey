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
    public class Player_Stick : Stick
    {
        Vector2 PreviousMousePostion;
        public Player_Stick(NewGame game)
            : base(game)
        {
            Velocity = new Vector2(0, 0);
            PreviousMousePostion = new Vector2(0, 0);
        }

        public void LoadContent()
        {
            Position = new Vector2(game.GameTable.TableTopLeft.X + Table.WIDTH - 70 - RADIUS, game.GameTable.TableTopLeft.Y + Table.HEIGHT / 2);
            base.LoadContent();
        }

        override public void Movement()
        {
            MouseState MOUSE = Mouse.GetState();
            Vector2 CurrentMousePosition = new Vector2(MOUSE.X, MOUSE.Y);
            Velocity = (CurrentMousePosition - PreviousMousePostion);
            base.Movement();
            PreviousMousePostion = CurrentMousePosition;
            Position.X = Math.Min(Position.X, (Table.WIDTH / 2) - RADIUS);
            base.Hit();
        }
    }
}