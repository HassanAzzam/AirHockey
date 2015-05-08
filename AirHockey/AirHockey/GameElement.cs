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
        protected GameApplication App;
        protected NewGame Game;
        public Vector2 Velocity; //Speed has been changed to Velocity by Alaa
        public Vector2 Position;
        protected Texture2D Texture;
        public float Radius;
        public float Mass; //Mass to calculate Forces

        public GameElement(GameApplication App,NewGame game)
            : base(App)
        {
            this.App = App;
            this.Game = game;
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
            Element.Position += Velocity;
            if(PuckInGoal(Element)){
                return;
            }
            Element.Position.X = Math.Max(Math.Min(Element.Position.X, Table.Width - Table.Thickness - Element.Radius), Table.Thickness + Element.Radius);
            Element.Position.Y = Math.Max(Math.Min(Element.Position.Y, Table.Height - Table.Thickness - Element.Radius), Table.Thickness + Element.Radius);
        }

        private bool PuckInGoal(GameElement Element){
            try{
                Puck tmp = Element as Puck;
                if (tmp.Position.Y >= Game.GameTable.GoalY_Start && tmp.Position.Y <= Game.GameTable.GoalY_End && (tmp.Position.X <= Table.Thickness || tmp.Position.X >= Table.Width - Table.Thickness))
                {
                    if (Element.Position.X < Table.Width / 2) Element.Position.X = Table.Thickness;
                    else Element.Position.X = Table.Width - Table.Thickness;
                    return true;
                }
                if (tmp.Position.Y >= Game.GameTable.GoalY_Start && tmp.Position.Y <= Game.GameTable.GoalY_End && (tmp.Position.X <= Table.Thickness + Element.Radius || tmp.Position.X >= Table.Width - Table.Thickness - Element.Radius))
                {
                    return true;
                }
                
            }
            catch
            {
                return false;
            }
            return false;
        }
    }
}