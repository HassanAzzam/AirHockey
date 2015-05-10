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
    public enum AppState
    {
        GameRunning,
        GameOver,
        InstructionsWindow,
        MainMenu,
        PauseMenu
    }

    public class GameApplication : Microsoft.Xna.Framework.Game
    {
        #region XNA objects
        public GraphicsDeviceManager Graphics;
        public SpriteBatch SpriteBatch;
        public SpriteFont SpriteFont;
        public SoundEffect PuckHitGoal;
        public SoundEffect PuckSound;
        public Texture2D GoalTexture;
        public Texture2D MuteTexture;
        public Texture2D UnMuteTexture;
        #endregion

        #region Application objects
        public AppState AppState;
        private GameApplication Application;
        private GameOver GameEnd;
        private InstructionsWindow InstructionsWindow;
        private MainMenu MainMenu;
        public NewGame Game;
        private PauseMenu PauseMenu;
        private Stopwatch StopWatch;
        public UI UI;
        #endregion

        public bool Mute;

        public GameApplication()
        {
            #region XNA.
            this.IsMouseVisible = true;
            this.IsFixedTimeStep = true;
            this.Graphics = new GraphicsDeviceManager(this);
            this.Graphics.PreferMultiSampling = true;
            this.Graphics.PreferredBackBufferHeight = 768;
            this.Graphics.PreferredBackBufferWidth = 1366;
            this.Graphics.IsFullScreen = true;
            this.Graphics.ApplyChanges();
            this.Content.RootDirectory = "Content";
            #endregion

            #region Application.
            this.AppState = AppState.MainMenu;
            this.Application = this;
            this.GameEnd = new GameOver();
            this.InstructionsWindow = new InstructionsWindow();
            this.MainMenu = new MainMenu();
            this.Game = new NewGame(ref this.Application);
            this.PauseMenu = new PauseMenu();
            this.StopWatch = new Stopwatch();
            this.UI = new UI(ref this.Application);
            #endregion

            this.Mute = false;
        }

        protected override void Initialize()
        {
            this.Game.AddComponents();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            this.SpriteBatch = new SpriteBatch(GraphicsDevice);
            this.SpriteFont = this.Content.Load<SpriteFont>("micross");
            this.PuckHitGoal = this.Content.Load<SoundEffect>("puck_hit_goal");
            this.PuckSound = this.Content.Load<SoundEffect>("PuckSound");
            this.GoalTexture = this.Content.Load<Texture2D>("Goal");
            this.MuteTexture = this.Content.Load<Texture2D>("Mute");
            this.UnMuteTexture = this.Content.Load<Texture2D>("UnMute");
            base.LoadContent();
        }

        protected override void UnloadContent()
        {

        }

        protected override void Update(GameTime GameTime)
        {
            if (this.AppState == AppState.MainMenu)
            {
                short UserChoise;
                this.IsMouseVisible = true;
                UserChoise = this.MainMenu.GetState();
                if (UserChoise == 1)
                {
                    this.Game.Start();
                    this.AppState = AppState.GameRunning;
                }
                else if (UserChoise == 2)
                {
                    this.AppState = AppState.InstructionsWindow;
                }
                else if (UserChoise == 3)
                {
                    this.Exit();
                }
                return;
            }
            if (this.AppState == AppState.InstructionsWindow)
            {
                this.IsMouseVisible = true;
                if (this.InstructionsWindow.GetState())
                {
                    this.AppState = AppState.MainMenu;
                }
                return;
            }
            if (this.AppState == AppState.PauseMenu)
            {
                short UserChoise;
                this.IsMouseVisible = true;
                UserChoise = this.PauseMenu.GetState();
                if (UserChoise == 1)
                {
                    this.AppState = AppState.GameRunning;
                    this.Game.GameState = GameState.Running;
                    Mouse.SetPosition((int)this.Game.Player.Position.X, (int)this.Game.Player.Position.Y);
                }
                else if (UserChoise == 2)
                {
                    this.AppState = AppState.MainMenu;
                }
                return;
            }
            if (this.AppState == AppState.GameRunning)
            {
                this.IsMouseVisible = false;
                this.Game.Update(GameTime);
            }
            if (this.StopWatch.Elapsed.Seconds > 1 && this.AppState == AppState.GameOver)
            {
                this.StopWatch.Reset();
                this.AppState = AppState.MainMenu;
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
            if (this.AppState == AppState.MainMenu)
            {
                this.MainMenu.Draw(ref this.Application);
                return;
            }

            if (this.AppState == AppState.InstructionsWindow)
            {
                this.InstructionsWindow.Draw(ref this.Application);
                return;
            }

            if (this.AppState == AppState.PauseMenu)
            {
                this.Game.Table.Draw();
                this.Game.CPU.Draw();
                this.Game.Player.Draw();
                this.Game.Puck.Draw();
                this.PauseMenu.Draw(ref this.Application);
                return;
            }

            this.Game.Draw();

            if (this.AppState == AppState.GameOver)
            {
                if (StopWatch.Elapsed.TotalMilliseconds == 0)
                {
                    this.StopWatch.Restart();
                }
                if (this.Game.GetWinner() == Winner.Player)
                {
                    GameOver.Win(ref this.Application);
                }
                else
                {
                    GameOver.Lose(ref this.Application);
                }
                return;
            }

            if (!this.Mute)
            {
                this.SpriteBatch.Draw(this.UnMuteTexture, new Vector2(Table.Thickness + 15, Table.Height + 15), Color.White);
            }
            else
            {
                this.SpriteBatch.Draw(this.MuteTexture, new Vector2(Table.Thickness + 15, Table.Height + 15), Color.White);
            }
        }
    }
}