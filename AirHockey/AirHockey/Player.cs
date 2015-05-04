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
    public class Player : DrawableGameComponent
    {
        
        public Player_Stick PLAYER_STICK;
        private Score PLAYER_SCORE;
        public Player(NewGame game) : base(game)
        {
            PLAYER_STICK = new Player_Stick(game);
            PLAYER_SCORE = new Score();
        }
        protected override void LoadContent()
        {
            PLAYER_STICK.LoadContent();
        }

        public void Move(GameTime Time)
        {
            PLAYER_STICK.Move(Time);
        }
    }
}
