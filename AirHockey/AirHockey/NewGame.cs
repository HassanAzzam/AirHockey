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
        #region XNA objects
        public GraphicsDeviceManager Graphics;
        private Texture2D GoalTex;
        private Texture2D UnMuteTex;
        private Texture2D MuteTex;
        public SpriteBatch SpriteBatch;
        public SpriteFont Font;
        private Stopwatch StopWatch;
        public SoundEffect PuckHitGoal;
        public SoundEffect PuckSound;
        #endregion

        #region Game objects
        private MainMenu NewMenu;
        public Table GameTable;
        public Player NewPlayer;
        public CPU NewCPU;
        public Puck NewPuck;
        private Scoreboard NewScoreboard;
        #endregion

        #region Flags
        private bool MenuTime;
        private bool Paused;
        private bool Goal;
        public bool Mute;
        private bool GameOver;
        #endregion

        private const short MAX_GOAL = 7;

        public NewGame()
        {
            this.Graphics = new GraphicsDeviceManager(this);
            this.Graphics.PreferMultiSampling = true;
            this.Graphics.PreferredBackBufferHeight = 768;
            this.Graphics.PreferredBackBufferWidth = 1366;
            this.Graphics.IsFullScreen = true;
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            this.Graphics.ApplyChanges();
            this.Content.RootDirectory = "Content";

            #region Initialization
            this.StopWatch = new Stopwatch();
            this.StopWatch.Start();
            this.NewMenu = new MainMenu();
            this.GameTable = new Table(this);
            this.NewPlayer = new Player(this);
            this.NewCPU = new CPU(this);
            this.NewPuck = new Puck(this);
            this.NewScoreboard = new Scoreboard(this);
            this.MenuTime = true;
            this.Paused = false;
            this.Goal = false;
            this.Mute = false;
            this.GameOver = false;
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
            this.Components.Add(GameTable);
            this.Components.Add(NewPuck);
            this.Components.Add(NewPlayer);
            this.Components.Add(NewCPU);
            this.Components.Add(NewScoreboard);

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // TODO: use this.Content to load your game content here
            // Create a new SpriteBatch, which can be used to draw textures.
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Font = Content.Load<SpriteFont>("micross");
            this.GoalTex = Content.Load<Texture2D>("Goal");
            this.UnMuteTex = Content.Load<Texture2D>("UnMute");
            this.MuteTex = Content.Load<Texture2D>("Mute");
            this.PuckHitGoal = Content.Load<SoundEffect>("puck_hit_goal");
            this.PuckSound = Content.Load<SoundEffect>("PuckSound");
            this.StartNewGame();
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
        /// <param name="GameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime GameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            if (this.MenuTime)
            {
                IsMouseVisible = true;
                short UserChoise = this.NewMenu.GetState();
                if (UserChoise == 1)
                {
                    Mouse.SetPosition((int)NewPlayer.Position.X, (int)NewPlayer.Position.Y);
                    this.StartNewGame();
                    this.IsMouseVisible = false;
                    this.MenuTime = false;
                    this.GameOver = false;
                    this.NewPlayer.Score = 0;
                    this.NewCPU.Score = 0;
                }
                else if (UserChoise == 2)
                {

                }
                else if (UserChoise == 3 || Keyboard.GetState().IsKeyDown(Keys.Escape))
                {
                    this.Exit();
                }
                return;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                this.Mute = !this.Mute;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && this.StopWatch.Elapsed.TotalSeconds >= 1 && !this.Goal)
            {
                this.Paused = !Paused;
                if (!Paused)
                {
                    Mouse.SetPosition((int)NewPlayer.Position.X, (int)NewPlayer.Position.Y);
                }
                this.StopWatch.Restart();
            }

            if (this.StopWatch.Elapsed.TotalSeconds >= 3 && this.GameOver)
            {
                this.MenuTime = true;
                this.StopWatch.Restart();
            }

            if (this.StopWatch.Elapsed.TotalSeconds >= 1 && this.Goal)
            {
                this.Goal = false;
                this.StartNewGame();
            }

            if (!this.Paused && !this.Goal && !this.GameOver)
            {
                this.NewPlayer.Move(GameTime);
                this.NewCPU.Move(GameTime);
                this.NewPuck.Move(GameTime);
            }
            base.Update(GameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="GameTime">Provides a snapshot of timing values.</param>
        /// 
        protected override void Draw(GameTime GameTime)
        {
            this.GraphicsDevice.Clear(Color.White);
            this.SpriteBatch.Begin();
            this.DrawElements();
            this.SpriteBatch.End();
            base.Draw(GameTime);
        }

        private void DrawElements()
        {
            if (this.MenuTime)
            {
                NewGame Game = this;
                this.NewMenu.Draw(ref Game);
                return;
            }
            this.GameTable.Draw();
            this.NewPuck.Draw();
            this.NewPlayer.Draw();
            this.NewCPU.Draw();
            this.NewScoreboard.Draw();
            if (this.Paused)
            {
                //this.DrawPause();
            }
            if (this.Goal)
            {
                this.SpriteBatch.Draw(this.GoalTex, new Vector2(Table.Width / 2 - this.GoalTex.Width / 2, Table.Height / 2 - this.GoalTex.Height / 2), Color.White);
            }
            if (this.GameOver)
            {
                this.EndGame();
                return;
            }
            if (!this.Mute)
            {
                this.SpriteBatch.Draw(this.UnMuteTex, new Vector2(Table.Thickness + 15, Table.Height + 15), Color.White);
            }
            if (this.Mute)
            {
                this.SpriteBatch.Draw(this.MuteTex, new Vector2(Table.Thickness + 15, Table.Height + 15), Color.White);
            }
        }

        public void StartNewGame()
        {
            Mouse.SetPosition((int)this.NewPlayer.Position.X, (int)this.NewPlayer.Position.Y);
            this.NewPuck.Initialize();
            this.NewPlayer.Initialize();
            this.NewCPU.Initialize();
        }

        public void GoalScored()
        {
            if (this.NewPlayer.Score == MAX_GOAL || this.NewCPU.Score == MAX_GOAL)
            {
                this.GameOver = true;
            }
            else
            {
                this.Goal = true;
            }
            this.StopWatch.Restart();
        }

        private void EndGame()
        {
            if (this.NewPlayer.Score == MAX_GOAL)
            {
                Vector2 Position = new Vector2(this.Graphics.GraphicsDevice.Viewport.Width / 2, this.Graphics.GraphicsDevice.Viewport.Height / 2);
                this.DrawBackground(Color.White, 1f);
                this.DrawBackground(Color.Green, 0.8f);
                this.DrawText("YOU WIN!", Color.White, Position, 1f);
            }
            else
            {
                Vector2 Position = new Vector2(this.Graphics.GraphicsDevice.Viewport.Width / 2, this.Graphics.GraphicsDevice.Viewport.Height / 2);
                this.DrawBackground(Color.White, 1f);
                this.DrawBackground(Color.Red, 0.8f);
                this.DrawText("GAME OVER", Color.White, Position, 1f);
            }
        }

        public void DrawText(string Text, Color Color, Vector2 Position, float Scale)
        {
            Vector2 Origin = this.Font.MeasureString(Text) / 2;
            this.SpriteBatch.DrawString(this.Font, Text, Position, Color, 0, Origin, Scale, SpriteEffects.None, 0.5f);
        }

        private void DrawBackground(Color Color, float Alpha)
        {
            Texture2D Rectangle = new Texture2D(this.Graphics.GraphicsDevice, 1, 1, true, SurfaceFormat.Color);
            Rectangle.SetData<Color>(new[] { Color * Alpha });
            this.SpriteBatch.Draw(Rectangle, new Rectangle(0, 0, this.Graphics.GraphicsDevice.Viewport.Width, this.Graphics.GraphicsDevice.Viewport.Height), Color.White);
        }
    }
}