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
        private Vector2 PositionPlayer,PositionCPU;
        private Texture2D TEXTURE;
        NewGame game;

        public Scoreboard(NewGame game) : base(game)
        {
            this.game = game;
            PositionPlayer = new Vector2(609.035f,690);
            PositionCPU = new Vector2(687.110f, 690);
        }

        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Scoreboard");
            base.LoadContent();
        }
        public void Draw()
        {
            game.spriteBatch.Draw(TEXTURE,PositionPlayer,Color.White);
            Vector2 FontOrigin = game.Font.MeasureString(game.NewPlayer.Score.ToString()) / 2;
            game.spriteBatch.DrawString(game.Font, game.NewPlayer.Score.ToString(), new Vector2(PositionPlayer.X + TEXTURE.Width / 2, PositionPlayer.Y + TEXTURE.Height / 2), Color.White, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);

            game.spriteBatch.Draw(TEXTURE, PositionCPU, Color.White);
            FontOrigin = game.Font.MeasureString(game.NewCPU.Score.ToString()) / 2;
            game.spriteBatch.DrawString(game.Font, game.NewCPU.Score.ToString(), new Vector2(PositionCPU.X + TEXTURE.Width / 2, PositionCPU.Y + TEXTURE.Height / 2), Color.White, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);
       
        }
    }
}
