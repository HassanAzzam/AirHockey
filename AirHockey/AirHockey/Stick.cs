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
    public abstract class Stick : GameElement
    {
        public Stick(NewGame game)  : base(game)
        {
            RADIUS = 19.5F;
            Position = new Vector2(0, 0);
            game.Content.RootDirectory = "Content";
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Stick");
            base.LoadContent();
        }

        public void Draw()
        {
            game.spriteBatch.Draw(this.TEXTURE, game.GameTable.TableTopLeft + this.Position - new Vector2(RADIUS, RADIUS), Color.White);
        }

        public void Hit()
        {
            double Distance = (game.NewDisc.RADIUS + RADIUS) - (game.NewDisc.Position - Position).Length();
            if (Distance > 0)
            {
                double Angle = Math.Atan2((-1 * Speed.Y), (-1 * Speed.X));
                Position = new Vector2((float)(Position.X + Distance * Math.Cos(Angle)), (float)(Position.Y + Distance * Math.Sin(Angle)));
                if (Speed != new Vector2(0, 0)) game.NewDisc.Speed = Speed;
                else { game.NewDisc.Speed.X *= -1; game.NewDisc.Speed.Y *= -1; }
            }
        }
    }
}
