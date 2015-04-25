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
    public class CPU : DrawableGameComponent
    {
        public CPU_Stick CPU_STICK;
        private Score CPU_SCORE;
        public CPU(NewGame game) : base(game)
        {
            CPU_STICK = new CPU_Stick(game);
            CPU_SCORE = new Score();
        }

        protected override void LoadContent()
        {

            CPU_STICK.LoadContent();
        }

    }
}
// GitHub Test