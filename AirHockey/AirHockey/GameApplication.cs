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

    public enum AppState
    {
        MainMenu,
        GameRunning,
        GameOver
    }

    public class GameApplication : Microsoft.Xna.Framework.Game
    {
        #region XNA objects
        public GraphicsDeviceManager Graphics;
        public Texture2D GoalTex;
        public Texture2D UnMuteTex;
        public Texture2D MuteTex;
        public SpriteBatch SpriteBatch;
        public SpriteFont Font;
        public SoundEffect PuckHitGoal;
        public SoundEffect PuckSound;
        #endregion

        #region Application objects
        private MainMenu NewMenu;
        public NewGame Game;
        private GameOver GameEnd;
        public AppState State;
        public UI UIDesigner;
        Stopwatch StopWatch;
        #endregion

        #region Flags
        public bool Mute;
        #endregion

        public GameApplication()
        {
            this.Graphics = new GraphicsDeviceManager(this);
            this.Graphics.PreferMultiSampling = true;
            this.Graphics.PreferredBackBufferHeight = 768;
            this.Graphics.PreferredBackBufferWidth = 1366;
            this.Graphics.IsFullScreen = false;
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            this.Graphics.ApplyChanges();
            this.Content.RootDirectory = "Content";

            #region Initialization
            this.State = AppState.MainMenu;
            this.NewMenu = new MainMenu();
            this.Game = new NewGame(this);
            this.GameEnd = new GameOver();
            this.UIDesigner = new UI(this);
            this.StopWatch = new Stopwatch();
            this.Mute = false;
            #endregion
        }

        protected override void Initialize()
        {
            Game.AddComponents();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.Font = Content.Load<SpriteFont>("micross");
            this.GoalTex = Content.Load<Texture2D>("Goal");
            this.UnMuteTex = Content.Load<Texture2D>("UnMute");
            this.MuteTex = Content.Load<Texture2D>("Mute");
            this.PuckHitGoal = Content.Load<SoundEffect>("puck_hit_goal");
            this.PuckSound = Content.Load<SoundEffect>("PuckSound");
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime GameTime)
        {
            // Allows the game to exit
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
            {
                this.Exit();
            }
            if (this.State == AppState.MainMenu)
            {
                IsMouseVisible = true;
                short UserChoise = this.NewMenu.GetState();
                if (UserChoise == 1)
                {
                    Game.Start();
                    State = AppState.GameRunning;
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
            else if (State == AppState.GameRunning)
            {
                Game.Update(GameTime);
            }

            if (StopWatch.Elapsed.Seconds>1 && State == AppState.GameOver)
            {
                StopWatch.Reset();
                State = AppState.MainMenu;
            }

            base.Update(GameTime);
        }

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
            if (this.State==AppState.MainMenu)
            {
                GameApplication Game = this;
                this.NewMenu.Draw(ref Game);
                return;
            }

            this.Game.Draw();
            
            
            if (this.State == AppState.GameOver)
            {
                if (StopWatch.Elapsed.TotalMilliseconds==0)
                {
                    StopWatch.Restart();
                }
                if (this.Game.GetWinner() == Winner.Player)
                {
                    GameOver.Win(this);
                }
                else
                {
                    GameOver.Lose(this);
                }
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

        
    }
}