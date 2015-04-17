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
        Vector2 pos;
        protected Texture2D TEXTURE;
        protected NewGame game;
        public GameElement(NewGame game) : base(game)
    {
        this.game = game;
        pos = new Vector2(0, 0);
    }
        public double Speed
        {
            get
            {
                throw new System.NotImplementedException();
            }
            set
            {
            }
        }

        public Vector2 Position
        {
            get
            {
               return pos;
            }
            set
            {
               pos = value;
            }
        }

        public virtual void Movement()
        {
            throw new System.NotImplementedException();
        }
    }
}
