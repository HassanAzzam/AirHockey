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
    public class Player : User
    {

        private PlayerPaddle PlayerPaddle;
        public int Score;
        public Player(GameApplication App,NewGame Game)
            : base(App,Game)
        {
            this.PlayerPaddle = new PlayerPaddle(App,Game);
        }

        protected override void LoadContent()
        {
            this.PlayerPaddle.LoadContent();
        }

        public override void Initialize()
        {
            this.LoadContent();
            this.PlayerPaddle.Velocity = Vector2.Zero;
            this.PlayerPaddle.Position = new Vector2(70, Table.Height / 2);
        }

        public override void Move(GameTime Time)
        {
            this.PlayerPaddle.Move(Time);
        }

        public override void Draw()
        {
            this.PlayerPaddle.Draw();
        }

        public override Vector2 Position
        {
            get
            {
                return this.PlayerPaddle.Position;
            }
            set
            {
                this.PlayerPaddle.Position = value;
            }
        }
        public override Vector2 Velocity
        {
            get
            {
                return this.PlayerPaddle.Velocity;
            }
            set
            {
                this.PlayerPaddle.Velocity = value;
            }
        }
        public override float Radius
        {
            get
            {
                return this.PlayerPaddle.Radius;
            }
        }
        public override float Mass
        {
            get
            {
                return this.PlayerPaddle.Mass;
            }
        }
    }
}