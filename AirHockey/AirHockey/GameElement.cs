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
        protected NewGame Game;
        public Vector2 Velocity; //Speed has been changed to Velocity by Alaa
        public Vector2 Position;
        protected Texture2D Texture;
        public float Radius;
        public float Mass; //Mass to calculate Forces

        public GameElement(NewGame Game)
            : base(Game)
        {
            this.Game = Game;
            this.Position = new Vector2(0, 0);
        }

        public virtual void Initialize()
        {

        }

        public virtual void Move(GameTime Time)
        {

        }

        protected void BoundPositionInTable(GameElement Element, Vector2 Velocity)
        {
            Element.Position.X = Math.Max(Math.Min(Element.Position.X + Velocity.X, Table.Width - Table.Thickness - Element.Radius), Table.Thickness + Element.Radius);
            Element.Position.Y = Math.Max(Math.Min(Element.Position.Y + Velocity.Y, Table.Height - Table.Thickness - Element.Radius), Table.Thickness + Element.Radius);
        }
    }
}