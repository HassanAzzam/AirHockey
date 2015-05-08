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
        public Puck NewPuck;
        private Menu NewMenu;
        public GraphicsDeviceManager graphics;
        public SpriteBatch spriteBatch;
        public SpriteFont Font;
        bool Paused;
        private Stopwatch STOP;
        private Scoreboard NewScoreboard;
        public Texture2D GoalTex;
        private Texture2D UnMuteTex;
        private Texture2D MuteTex;
        public bool Goal = false;
        public bool Mute = false;
        private bool MenuTime;
        private bool GameOver = false;
        private const int MAX_GOAL = 5;

        #region Sound Effect
        public SoundEffect PuckHitGoal;
        public SoundEffect PuckSound;
        #endregion

        public NewGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferMultiSampling = true;
            Content.RootDirectory = "Content";
            graphics.PreferredBackBufferHeight = 768;
            graphics.PreferredBackBufferWidth = 1366;
            graphics.IsFullScreen = true;
            IsMouseVisible = true;
            IsFixedTimeStep = true;
            graphics.ApplyChanges();
            //TargetElapsedTime = TimeSpan.FromMilliseconds(5);

            #region Initialization
            Paused = false;
            STOP = new Stopwatch();
            STOP.Start();
            NewScoreboard = new Scoreboard(this);
            GameTable = new Table(this);
            NewCPU = new CPU(this);
            NewPlayer = new Player(this);
            NewPuck = new Puck(this);
            this.NewMenu = new Menu();
            this.MenuTime = true;
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
            Components.Add(NewPuck);
            Components.Add(NewPlayer);
            Components.Add(NewCPU);
            Components.Add(NewScoreboard);

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
            Font = Content.Load<SpriteFont>("micross");
            GoalTex = Content.Load<Texture2D>("Goal");
            UnMuteTex = Content.Load<Texture2D>("UnMute");
            MuteTex = Content.Load<Texture2D>("Mute");
            PuckHitGoal = Content.Load<SoundEffect>("puck_hit_goal");
            PuckSound = Content.Load<SoundEffect>("PuckSound");
            initialize();
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
            if (this.MenuTime)
            {
                IsMouseVisible = true;
                GameOver = false;
                short UserChoise = this.NewMenu.GetState();
                if (UserChoise == -1 || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
                else if (UserChoise == 1)
                {
                    IsMouseVisible = false;
                    //this.Background(Color.White);
                    this.MenuTime = false;
                    initialize();
                    Mouse.SetPosition((int)NewPlayer.Position.X, (int)NewPlayer.Position.Y);
                    NewPlayer.Score = NewCPU.Score = 0;
                    STOP.Restart();
                }
                return;
            }

            #region Mute & Unmute
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                Mute = true;
            }
            if (Keyboard.GetState().IsKeyDown(Keys.U))
            {
                Mute = false;
            }
            #endregion

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && STOP.Elapsed.TotalSeconds >= 1 && !Goal)
            {
                Paused = !Paused;
                if (!Paused)
                {
                    Mouse.SetPosition((int)NewPlayer.Position.X, (int)NewPlayer.Position.Y);
                }
                STOP.Restart();
            }
            
                if (STOP.Elapsed.TotalSeconds >= 3 &&GameOver)
                {
                    MenuTime = true;
                    STOP.Restart();
                }
            

            if (STOP.Elapsed.TotalSeconds >= 1 && Goal)
            {
                Goal = false;
                initialize();
            }
            
            if (!Paused && !Goal && !GameOver)
            {
                NewPlayer.Move(gameTime);
                NewCPU.Move(gameTime);
                NewPuck.Move(gameTime);
            }
            base.Update(gameTime);
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
            if (this.MenuTime)
            {
                NewGame Game = this;
                this.NewMenu.Draw(ref Game);
                return;
            }
            GameTable.Draw();
            NewPuck.Draw();
            NewPlayer.Draw();
            NewCPU.Draw();
            NewScoreboard.Draw();
            if (Paused)
            {
                DrawPause();
            }
            if (Goal == true)
            {
                spriteBatch.Draw(GoalTex, new Vector2(Table.WIDTH / 2 - GoalTex.Width / 2, Table.HEIGHT / 2 - GoalTex.Height / 2), Color.White);
            }
            if (GameOver)
            {
                DrawEnd();
                return;
            }
            if (!Mute)
            {
                spriteBatch.Draw(UnMuteTex, new Vector2(Table.Thickness + 15, Table.HEIGHT + 15), Color.White);
            }
            if (Mute)
            {
                spriteBatch.Draw(MuteTex, new Vector2(Table.Thickness + 15, Table.HEIGHT + 15), Color.White);
            }
        }

        private void DrawPause()
        {
            //Drawing Rectangle
            Texture2D rec = new Texture2D(graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            rec.SetData<Color>(new[] { Color.Black * 0.5f });
            spriteBatch.Draw(rec, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);

            //Drawing Text
            Vector2 PausePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Vector2 FontOrigin = Font.MeasureString("PAUSED") / 2;
            spriteBatch.DrawString(Font, "PAUSED", PausePos, Color.White, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
            PausePos.Y = graphics.GraphicsDevice.Viewport.Height - 50;
            FontOrigin = Font.MeasureString("Press ESC to Resume") / 2;
            spriteBatch.DrawString(Font, "Press ESC to Resume", PausePos, Color.Black, 0, FontOrigin, 0.2f, SpriteEffects.None, 0.5f);
        }

        public void initialize()
        {
            NewPuck.Initialize();
            NewPlayer.Initialize();
            NewCPU.Initialize();
            Mouse.SetPosition((int)NewPlayer.Position.X, (int)NewPlayer.Position.Y);
        }

        public void GoalScored()
        {
            if (NewPlayer.Score == MAX_GOAL || NewCPU.Score == MAX_GOAL)
            {
                GameOver = true;
            }
            else
            {
                Goal = true;
            }
            STOP.Restart();
        }

        private void DrawEnd()
        {
            if (NewPlayer.Score == MAX_GOAL)
            {
                DrawBackground(Color.White, 1f);
                DrawBackground(Color.Green,0.8f);
                DrawText("YOU WIN!", Color.White);
            }
            else
            {
                DrawBackground(Color.White, 1f);
                DrawBackground(Color.Red,0.8f);
                DrawText("YOU LOSE!", Color.White);
            }
        }

        private void DrawText(string Text, Color COLOR)
        {
            Vector2 PausePos = new Vector2(graphics.GraphicsDevice.Viewport.Width / 2, graphics.GraphicsDevice.Viewport.Height / 2);
            Vector2 FontOrigin = Font.MeasureString(Text) / 2;
            spriteBatch.DrawString(Font, Text, PausePos, COLOR, 0, FontOrigin, 1.0f, SpriteEffects.None, 0.5f);
        }

        private void DrawBackground(Color COLOR, float Alpha)
        {
            Texture2D rec = new Texture2D(graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            rec.SetData<Color>(new[] { COLOR * Alpha });
            spriteBatch.Draw(rec, new Rectangle(0, 0, graphics.GraphicsDevice.Viewport.Width, graphics.GraphicsDevice.Viewport.Height), Color.White);
        }
    }
}
