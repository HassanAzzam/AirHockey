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
    public class Scoreboard : DrawableGameComponent
    {
        private Point Position;
        private Texture2D TEXTURE;
        NewGame game;

        public Scoreboard(NewGame game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Scoreboard");
            base.LoadContent();
        }
        public void Draw()
        {
            const int x= 600;
            game.spriteBatch.Draw(TEXTURE,new Vector2(x-100,x),Color.White);
            Vector2 FontOrigin = game.Font.MeasureString(game.NewPlayer.Points.ToString()) / 2;
            game.spriteBatch.DrawString(game.Font, game.NewPlayer.Points.ToString(), new Vector2(x+35-100,x+40), Color.White, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);

            game.spriteBatch.Draw(TEXTURE, new Vector2(x, x), Color.White);
            FontOrigin = game.Font.MeasureString(game.NewCPU.Points.ToString()) / 2;
            game.spriteBatch.DrawString(game.Font, game.NewCPU.Points.ToString(), new Vector2(x + 35, x + 40), Color.White, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);
       
        }
    }
}
