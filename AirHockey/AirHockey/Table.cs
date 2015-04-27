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
        public static int HEIGHT;
        public static int WIDTH;
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
            HEIGHT=TEXTURE.Height;
            WIDTH=TEXTURE.Width;
            base.LoadContent();
        }
        public void Draw(){
            game.spriteBatch.Draw(this.TEXTURE, TableTopLeft, Color.White);
        }

        public bool Goal(float X, float Y)
        {
            if (X <= game.NewDisc.RADIUS + 20 + game.NewDisc.RADIUS
                && Y >= game.NewDisc.RADIUS + game.GameTable.TableTopLeft.Y + 175 + game.NewDisc.RADIUS
                && Y <= game.NewDisc.RADIUS + game.GameTable.TableTopLeft.Y + 330 - game.NewDisc.RADIUS)
            {
                return true;
            }
            if (X <= Table.WIDTH - game.NewDisc.RADIUS - 20
               && Y >= game.NewDisc.RADIUS + game.GameTable.TableTopLeft.Y + 175 + game.NewDisc.RADIUS
                && Y <= game.NewDisc.RADIUS + game.GameTable.TableTopLeft.Y + 330 - game.NewDisc.RADIUS)
            {
                return true;
            }
            return false;
        }
    }
}
