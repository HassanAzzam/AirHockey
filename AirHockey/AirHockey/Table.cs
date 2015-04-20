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
    public class Table : DrawableGameComponent
    {
        private Texture2D TEXTURE;
        public const int LENGTH=579;
        public const int WIDTH=278;
        public Vector2 TableTopLeft;
        public const float FRICTION=0.1F;
        NewGame game;
        public Table(NewGame game) : base(game)
        {
            this.game = game;
            game.Content.RootDirectory = "Content";
            TableTopLeft = new Vector2(0, 0);
        }
        protected override void LoadContent()
        {
            TEXTURE = game.Content.Load<Texture2D>("Table");
            base.LoadContent();
        }
        public void Draw(){
            game.spriteBatch.Draw(this.TEXTURE, TableTopLeft, Color.White);
        }
    }
}
