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

        public Vector2 Speed;

        public Vector2 Position;

        public float RADIUS;
       
        public virtual void Movement()
        {
            Position.X = Math.Max(Math.Min(Position.X + Speed.X, game.GameTable.TableTopLeft.X + Table.WIDTH - 15 - RADIUS), game.GameTable.TableTopLeft.X + 15 + RADIUS);
            Position.Y = Math.Max(Math.Min(Position.Y + Speed.Y, game.GameTable.TableTopLeft.Y + Table.LENGTH - 40 - RADIUS), game.GameTable.TableTopLeft.Y + 40 + RADIUS);
            
        }
    }
}
