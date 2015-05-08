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
        private CPUPaddle CPUPADDLE;
        public int Score;
        public CPU(NewGame Game)
            : base(Game)
        {
            this.CPUPADDLE = new CPUPaddle(Game);
        }

        protected override void LoadContent()
        {
            this.CPUPADDLE.LoadContent();
        }

        public override void Initialize()
        {
            this.LoadContent();
            this.CPUPADDLE.Velocity = Vector2.Zero;
            this.CPUPADDLE.Position = new Vector2(Table.Width - 70, Table.Height / 2);
        }

        public override void Move(GameTime Time)
        {
            this.CPUPADDLE.Move(Time);
        }

        public override void Draw()
        {
            this.CPUPADDLE.Draw();
        }

        public override Vector2 Position
        {
            get
            {
                return this.CPUPADDLE.Position;
            }
            set
            {
                this.CPUPADDLE.Position = value;
            }
        }
        public override Vector2 Velocity
        {
            get
            {
                return this.CPUPADDLE.Velocity;
            }
            set
            {
                this.CPUPADDLE.Velocity = value;
            }
        }
        public override float Radius
        {
            get
            {
                return this.CPUPADDLE.Radius;
            }
        }
        public override float Mass
        {
            get
            {
                return this.CPUPADDLE.Mass;
            }
        }
    }
}