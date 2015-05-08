using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class User : DrawableGameComponent
    {
        public User(GameApplication App,NewGame Game)
            : base(App)
        {

        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        public virtual void Move(GameTime Time)
        {

        }

        public virtual void Draw()
        {

        }

        public virtual Vector2 Position
        {
            get;
            set;
        }
        public virtual Vector2 Velocity
        {
            get;
            set;
        }
        public virtual float Radius
        {
            get
            {
                throw new NotImplementedException();
            }
        }
        public virtual float Mass
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}