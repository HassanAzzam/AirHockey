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
        private CPU_Stick CPU_STICK;
        private Score CPU_SCORE;
        public CPU(NewGame game) : base(game)
        {
            CPU_STICK = new CPU_Stick(game);
            CPU_SCORE = new Score(game);
        }

        protected override void LoadContent()
        {
            CPU_STICK.LoadContent();
        }

        public override void Initialize()
        {
            LoadContent();
            CPU_STICK.Velocity = Vector2.Zero;
            CPU_STICK.Position = new Vector2(Table.WIDTH - 70, Table.HEIGHT / 2);
        }

        public override void Move(GameTime Time)
        {
            CPU_STICK.Move(Time);
        }

        public override void Draw()
        {
            CPU_STICK.Draw();
        }

        public override int Points { get { return CPU_SCORE.Points; } set { CPU_SCORE.Points = value; } }
        public override Vector2 Position { get { return CPU_STICK.Position; } set { CPU_STICK.Position = value; } }
        public override Vector2 Velocity { get { return CPU_STICK.Velocity; } set { CPU_STICK.Velocity = value; } }
        public override float RADIUS { get { return CPU_STICK.RADIUS; } }
        public override float Mass { get { return CPU_STICK.Mass; } }
    }
}