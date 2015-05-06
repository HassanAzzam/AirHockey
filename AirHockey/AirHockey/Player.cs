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

        private Player_Paddle PLAYER_PADDLE;
        private Score PLAYER_SCORE;
        public Player(NewGame game) : base(game)
        {
            PLAYER_PADDLE = new Player_Paddle(game);
            PLAYER_SCORE = new Score(game);
        }
        protected override void LoadContent()
        {
            PLAYER_PADDLE.LoadContent();
        }

        public override void Initialize()
        {
            LoadContent();
            PLAYER_PADDLE.Velocity = Vector2.Zero;
            PLAYER_PADDLE.Position = new Vector2(70, Table.HEIGHT / 2);
        }

        public override void Move(GameTime Time)
        {
            PLAYER_PADDLE.Move(Time);
        }

        public override void Draw()
        {
            PLAYER_PADDLE.Draw();
        }

        public override int Points { get { return PLAYER_SCORE.Points; } set { PLAYER_SCORE.Points = value; } }
        public override Vector2 Position { get { return PLAYER_PADDLE.Position; } set { PLAYER_PADDLE.Position = value; } }
        public override Vector2 Velocity { get { return PLAYER_PADDLE.Velocity; } set { PLAYER_PADDLE.Velocity = value; } }
        public override float RADIUS { get { return PLAYER_PADDLE.RADIUS; } }
        public override float Mass { get { return PLAYER_PADDLE.Mass; } }
    }
}
