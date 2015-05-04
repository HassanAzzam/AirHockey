﻿using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class User : DrawableGameComponent
    {
        public User(NewGame game) : base(game)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public virtual void Move(GameTime Time){   }

        public virtual void Draw(){   }

        public virtual int Points { get; set; }
        public virtual Vector2 Position { get; set; }
        public virtual Vector2 Velocity { get; set; }
        public virtual float RADIUS { get { throw new NotImplementedException(); } }
        public virtual float Mass { get { throw new NotImplementedException(); } }
    }
}
