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
using System.Diagnostics;
using System.Threading;

namespace AirHockey
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    /// 
    public class NewGame : Microsoft.Xna.Framework.Game
    {
        public Table GameTable;
        public Player NewPlayer;
        public CPU NewCPU;
        public Disc NewDisc;
        GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        SpriteFont PauseFont;
        bool Paused;
        Stopwatch STOP,Time;

        public NewGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1266;
            graphics.IsFullScreen = false;
            IsMouseVisible = false;
            IsFixedTimeStep = false;
            //TargetElapsedTime = TimeSpan.FromMilliseconds(3);

            #region Initialization
            Paused = false;
            STOP = new Stopwatch();
            STOP.Start();
            GameTable = new Table(this);
            NewCPU = new CPU(this);
            NewPlayer = new Player(this);
            NewDisc = new Disc(this);
            #endregion


        }


        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Components.Add(GameTable);
            Components.Add(NewDisc);
            Components.Add(NewPlayer);
            Components.Add(NewCPU);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            PauseFont = Content.Load<SpriteFont>("micross");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
                this.Exit();
            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && STOP.Elapsed.TotalSeconds >= 1)
            {
                Paused = !Paused;
                if (!Paused)
                {
                    Mouse.SetPosition((int)NewPlayer.PLAYER_STICK.Position.X, (int)NewPlayer.PLAYER_STICK.Position.Y);
                }
                STOP.Restart();
            }
            if (!Paused)
            {
                NewPlayer.Move(gameTime);
                NewCPU.Move(gameTime);
                NewDisc.Move(gameTime);
            }
            base.Update(gameTime);
            //base.Draw(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            DrawElements();
            spriteBatch.End();
            base.Draw(gameTime);
        }

        private void DrawElements()
        {
            GameTable.Draw();
            NewDisc.Draw();
            NewPlayer.PLAYER_STICK.Draw();
            NewCPU.CPU_STICK.Draw();
            if (Paused)
            {
                //Drawing Rectangle
                Texture2D rec = new Texture2D(graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
                rec.SetData<Color>(new[] { Color.Black * 0.5f });
                spriteBatch.Draw(rec, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);

                //Drawing Text
                Vector2 PausePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
                Vector2 FontOrigin = PauseFont.MeasureString("PAUSED") / 2;
                spriteBatch.DrawString(PauseFont, "PAUSED", PausePos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
                PausePos.Y = graphics.GraphicsDevice.Viewport.Height - 50;
                FontOrigin = PauseFont.MeasureString("Press ESC to Resume") / 2;
                spriteBatch.DrawString(PauseFont, "Press ESC to Resume", PausePos, Color.Black, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);
            }
        }

    }
}
