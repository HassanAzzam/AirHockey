using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AirHockey
{
    public class CPUPaddle : Paddle
    {
        private Vector2 DefaultPosition;
        public CPUPaddle(GameApplication App,NewGame Game)
            : base(App,Game)
        {
            this.Velocity = Vector2.Zero;
        }

        public void LoadContent()
        {
            this.DefaultPosition = new Vector2(Table.Width - 70, Table.Height / 2);
            base.LoadContent();
        }

        public override void Move(GameTime Time)
        {
            if (this.Position.X <= this.Game.NewPuck.Position.X)
            {
                this.Defense();
            }
            else if (this.Game.NewPuck.Position.X >= Table.Width / 2 + this.App.Game.NewPuck.Radius)
            {
                this.Offense();
            }
            else
            {
                this.ReturnToDefaultPosition();
            }
            this.Velocity *= 0.2f; //Decrease Paddle's Velocity
            this.BoundPositionInTable(this, this.Velocity * Time.ElapsedGameTime.Milliseconds / 60f);
            this.Position.X = Math.Max(this.Position.X, Table.Width / 2 + this.Radius);
        }

        private void ReturnToDefaultPosition()
        {
            this.Velocity = (this.DefaultPosition - this.Position) * 1.3f;
        }

        private void Offense()
        {
            this.Velocity = this.Game.NewPuck.Position - this.Position;
        }

        private void Defense()
        {
            this.Velocity = this.DefaultPosition - this.Position;
            if (this.Game.NewPuck.Velocity.X > 0)
            {
                this.Velocity.X = this.Game.NewPuck.Velocity.X * 3.5f;
            }
            else
            {
                this.Velocity.X = Table.Width - Table.Thickness - this.Radius - this.Position.X;
            }
            this.Velocity *= 1.2f;
        }
    }
}