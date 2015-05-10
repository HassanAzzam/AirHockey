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
        private Vector2 PositionPlayer;
        private Vector2 PositionCPU;
        private Texture2D Texture;
        private GameApplication App;

        public Scoreboard(ref GameApplication App)
            : base(App)
        {
            this.App = App;
            this.PositionPlayer = new Vector2(609.035f, 690);
            this.PositionCPU = new Vector2(687.110f, 690);
        }


        public void Load()
        {
            this.LoadContent();
        }

        protected override void LoadContent()
        {
            this.Texture = this.App.Content.Load<Texture2D>("Scoreboard");
            base.LoadContent();
        }
        public void Draw()
        {
            Vector2 FontOrigin = this.App.SpriteFont.MeasureString(this.App.Game.Player.Score.ToString()) / 2;
            this.App.SpriteBatch.Draw(this.Texture, this.PositionPlayer, Color.White);
            this.App.SpriteBatch.DrawString(
                this.App.SpriteFont, this.App.Game.Player.Score.ToString(),
                new Vector2(
                    this.PositionPlayer.X + this.Texture.Width / 2,
                    this.PositionPlayer.Y + this.Texture.Height / 2),
                    Color.White,
                    0,
                    FontOrigin,
                    0.2f,
                    SpriteEffects.None, 0.5f);
            this.App.SpriteBatch.Draw(this.Texture, this.PositionCPU, Color.White);
            FontOrigin = this.App.SpriteFont.MeasureString(this.App.Game.CPU.Score.ToString()) / 2;
            this.App.SpriteBatch.DrawString(
                this.App.SpriteFont,
                this.App.Game.CPU.Score.ToString(),
                new Vector2(
                    this.PositionCPU.X + this.Texture.Width / 2,
                    this.PositionCPU.Y + this.Texture.Height / 2),
                    Color.White,
                    0,
                    FontOrigin,
                    0.2f,
                    SpriteEffects.None,
                    0.5f);
        }
    }
}