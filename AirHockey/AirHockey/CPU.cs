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
    public class CPU : User
    {
        private CPUPaddle CPUPaddle;
        public int Score;
        public CPU(ref GameApplication App, ref NewGame Game) : base(ref App, ref Game)
        {
            this.CPUPaddle = new CPUPaddle(ref App, ref Game);
        }

        protected override void LoadContent()
        {
            this.CPUPaddle.LoadContent();
        }

        public override void Initialize()
        {
            this.LoadContent();
            this.CPUPaddle.Velocity = Vector2.Zero;
            this.CPUPaddle.Position = new Vector2(Table.Width - 70, Table.Height / 2);
        }

        public override void Move(GameTime Time)
        {
            this.CPUPaddle.Move(Time);
        }

        public override void Draw()
        {
            this.CPUPaddle.Draw();
        }

        public override Vector2 Position
        {
            get
            {
                return this.CPUPaddle.Position;
            }
            set
            {
                this.CPUPaddle.Position = value;
            }
        }
        public override Vector2 Velocity
        {
            get
            {
                return this.CPUPaddle.Velocity;
            }
            set
            {
                this.CPUPaddle.Velocity = value;
            }
        }
        public override float Radius
        {
            get
            {
                return this.CPUPaddle.Radius;
            }
        }
        public override float Mass
        {
            get
            {
                return this.CPUPaddle.Mass;
            }
        }
    }
}