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
        private CPU_Paddle CPU_PADDLE;
        private Score CPU_SCORE;
        public CPU(NewGame game) : base(game)
        {
            CPU_PADDLE = new CPU_Paddle(game);
            CPU_SCORE = new Score(game);
        }

        protected override void LoadContent()
        {
            CPU_PADDLE.LoadContent();
        }

        public override void Initialize()
        {
            LoadContent();
            CPU_PADDLE.Velocity = Vector2.Zero;
            CPU_PADDLE.Position = new Vector2(Table.WIDTH - 70, Table.HEIGHT / 2);
        }

        public override void Move(GameTime Time)
        {
            CPU_PADDLE.Move(Time);
        }

        public override void Draw()
        {
            CPU_PADDLE.Draw();
        }

        public override int Points { get { return CPU_SCORE.Points; } set { CPU_SCORE.Points = value; } }
        public override Vector2 Position { get { return CPU_PADDLE.Position; } set { CPU_PADDLE.Position = value; } }
        public override Vector2 Velocity { get { return CPU_PADDLE.Velocity; } set { CPU_PADDLE.Velocity = value; } }
        public override float RADIUS { get { return CPU_PADDLE.RADIUS; } }
        public override float Mass { get { return CPU_PADDLE.Mass; } }
    }
}