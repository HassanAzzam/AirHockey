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
    public abstract class GameElement : DrawableGameComponent
    {
        protected Texture2D TEXTURE;
        protected NewGame game;

        public GameElement(NewGame game) : base(game)
        {
            this.game = game;
            Position = new Vector2(0, 0);
        }

        public virtual void Initialize()
        {

        }

        public Vector2 Velocity; //Speed has been changed to Velocity by Alaa

        public Vector2 Position;

        public float RADIUS,Mass; //Mass to calculate Forces

        public virtual void Move(GameTime Time)
        {
          
        }

        protected void BoundPositionInTable(GameElement Element, Vector2 _Velocity)
        {
            Element.Position.X = Math.Max(Math.Min(Element.Position.X + _Velocity.X, Table.WIDTH - Table.Thickness - Element.RADIUS), Table.Thickness + Element.RADIUS);
            Element.Position.Y = Math.Max(Math.Min(Element.Position.Y + _Velocity.Y, Table.HEIGHT - Table.Thickness - Element.RADIUS),Table.Thickness + Element.RADIUS);
        }
    }
}
