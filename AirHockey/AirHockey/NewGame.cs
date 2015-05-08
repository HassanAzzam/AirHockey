using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace AirHockey
{
    public enum GameState
    {
        Running,
        Paused,
        Goal
    }

    public enum Winner
    {
        Player,
        CPU
    }

    public class NewGame
    {
        GameApplication App;

        #region Game objects

        public Table GameTable;
        public Player NewPlayer;
        public CPU NewCPU;
        public Puck NewPuck;
        private Scoreboard NewScoreboard;
        private Stopwatch StopWatch;
        private GameState State;
        //pause menu

        #endregion

        private const short MAX_GOAL = 7;

        public NewGame(GameApplication App)
        {
            this.App = App;

            #region Initialization
            this.GameTable = new Table(App,this);
            this.NewPlayer = new Player(App,this);
            this.NewCPU = new CPU(App,this);
            this.NewPuck = new Puck(App,this);
            this.NewScoreboard = new Scoreboard(App);
            this.StopWatch = new Stopwatch();
            //pause menu
            #endregion

        }

        public void AddComponents()
        {
            App.Components.Add(GameTable);
            App.Components.Add(NewPuck);
            App.Components.Add(NewPlayer);
            App.Components.Add(NewCPU);
            App.Components.Add(NewScoreboard);
        }

        public void Start()
        {

            this.Initialize();   
            Mouse.SetPosition((int)this.NewPlayer.Position.X, (int)this.NewPlayer.Position.Y);
            App.IsMouseVisible = false;
            this.StopWatch.Start();
            this.NewPlayer.Score = 0;
            this.NewCPU.Score = 0;

            State = GameState.Running;
        }

        private void Initialize()
        {
            this.NewPuck.Initialize();
            this.NewPlayer.Initialize();
            this.NewCPU.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (Keyboard.GetState().IsKeyDown(Keys.M))
            {
                App.Mute = !App.Mute;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Escape) && this.StopWatch.Elapsed.TotalSeconds >= 1 && State==GameState.Running)
            {
                
               // this.StopWatch.Restart();
            }

            if (State == GameState.Goal)
            {
                Thread.Sleep(1000);
                this.Initialize();
                if (this.NewPlayer.Score == MAX_GOAL || this.NewCPU.Score == MAX_GOAL)
                {
                    App.State = AppState.GameOver;
                }
                State = GameState.Running;
                Mouse.SetPosition((int)this.NewPlayer.Position.X, (int)this.NewPlayer.Position.Y);
            }

            if (State == GameState.Running)
            {
                this.NewPlayer.Move(gameTime);
                this.NewCPU.Move(gameTime);
                this.NewPuck.Move(gameTime);
            }
        }

        public void Draw()
        {
            this.GameTable.Draw();
            this.NewPuck.Draw();
            this.NewPlayer.Draw();
            this.NewCPU.Draw();
            this.NewScoreboard.Draw();
            this.App.SpriteBatch.Draw(this.App.GoalGrey, new Vector2(Table.TopLeft.X, Table.TopLeft.Y + Table.Height / 2 - App.GoalGrey.Height/2),Color.White);

            if (this.State == GameState.Paused)
            {
                //pause menu
            }

            if (this.State == GameState.Goal)
            {
                App.SpriteBatch.Draw(App.GoalTex, new Vector2(Table.Width / 2 - App.GoalTex.Width / 2, Table.Height / 2 - App.GoalTex.Height / 2), Color.White);
            }
        }

        public void GoalScored()
        {
            this.State = GameState.Goal;
           
        }

        public Winner GetWinner()
        {
            if (this.NewPlayer.Score == MAX_GOAL) return Winner.Player;
            return Winner.CPU;
        }
    }
}
