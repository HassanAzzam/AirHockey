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

        private Player_Stick PLAYER_STICK;
        private Score PLAYER_SCORE;
        public Player(NewGame game) : base(game)
        {
            PLAYER_STICK = new Player_Stick(game);
            PLAYER_SCORE = new Score(game);
        }
        protected override void LoadContent()
        {
            PLAYER_STICK.LoadContent();
        }

        public override void Initialize()
        {
            LoadContent();
            PLAYER_STICK.Velocity = Vector2.Zero;
            PLAYER_STICK.Position = new Vector2(Table.WIDTH - 70, Table.HEIGHT / 2);
        }

        public override void Move(GameTime Time)
        {
            PLAYER_STICK.Move(Time);
        }

        public override void Draw()
        {
            PLAYER_STICK.Draw();
        }

        public override int Points { get { return PLAYER_SCORE.Points; } set { PLAYER_SCORE.Points = value; } }
        public override Vector2 Position { get { return PLAYER_STICK.Position; } set { PLAYER_STICK.Position = value; } }
        public override Vector2 Velocity { get { return PLAYER_STICK.Velocity; } set { PLAYER_STICK.Velocity = value; } }
        public override float RADIUS { get { return PLAYER_STICK.RADIUS; } }
        public override float Mass { get { return PLAYER_STICK.Mass; } }
    }
}
