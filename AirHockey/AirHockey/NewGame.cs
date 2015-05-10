using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;

namespace AirHockey
{
    public enum GameState
    {
        Goal,
        Paused,
        Running
    }

    public enum Winner
    {
        Player,
        CPU
    }

    public class NewGame
    {
        public CPU CPU;
        private GameApplication Application;
        public GameState GameState;
        private NewGame Game;
        public Player Player;
        public Puck Puck;
        private Scoreboard Scoreboard;
        private Stopwatch StopWatch;
        public Table Table;
        private const short WinnerScore = 7;
        
        public NewGame(ref GameApplication Application)
        {
            this.Application = Application;
            this.Game = this;
            this.CPU = new CPU(ref this.Application, ref this.Game);
            this.GameState = GameState.Running;
            this.Player = new Player(ref this.Application, ref this.Game);
            this.Puck = new Puck(ref this.Application, ref this.Game);
            this.Scoreboard = new Scoreboard(ref this.Application);
            this.StopWatch = new Stopwatch();
            this.Table = new Table(ref this.Application, ref this.Game);
        }

        public void AddComponents()
        {
            this.Application.Components.Add(this.CPU);
            this.Application.Components.Add(this.Puck);
            this.Application.Components.Add(this.Player);
            this.Application.Components.Add(this.Scoreboard);
            this.Application.Components.Add(this.Table);
        }

        public void Start()
        {
            this.Initialize();
            Mouse.SetPosition((int)this.Player.Position.X, (int)this.Player.Position.Y);
            this.CPU.Score = 0;
            this.Application.IsMouseVisible = false;
            this.GameState = GameState.Running;
            this.Player.Score = 0;
            this.StopWatch.Start();
        }

        private void Initialize()
        {
            this.CPU.Initialize();
            this.Player.Initialize();
            this.Puck.Initialize();
        }

        public void Update(GameTime gameTime)
        {
            if (this.GameState == GameState.Running)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.P))
                {
                    this.Application.AppState = AppState.PauseMenu;
                    this.GameState = GameState.Paused;
                }
                else
                {
                    if (Keyboard.GetState().IsKeyDown(Keys.M))
                    {
                        this.Application.Mute = !this.Application.Mute;
                    }
                    this.CPU.Move(gameTime);
                    this.Player.Move(gameTime);
                    this.Puck.Move(gameTime);
                }
                return;
            }

            if (this.GameState == GameState.Goal)
            {
                Thread.Sleep(1000);
                this.Initialize();
                if (this.Player.Score == WinnerScore || this.CPU.Score == WinnerScore)
                {
                    this.Application.AppState = AppState.GameOver;
                }
                this.GameState = GameState.Running;
                Mouse.SetPosition((int)this.Player.Position.X, (int)this.Player.Position.Y);
            }
        }

        public void Draw()
        {
            this.Table.Draw();
            this.Puck.Draw();
            this.Player.Draw();
            this.CPU.Draw();
            this.Scoreboard.Draw();

            if (this.GameState == GameState.Goal)
            {
                this.Application.SpriteBatch.Draw(
                    this.Application.GoalTexture,
                    new Vector2(
                        Table.Width / 2 - this.Application.GoalTexture.Width / 2,
                        Table.Height / 2 - this.Application.GoalTexture.Height / 2),
                        Color.White);
            }
        }

        public void GoalScored()
        {
            this.GameState = GameState.Goal;
        }

        public Winner GetWinner()
        {
            if (this.Player.Score == WinnerScore)
            {
                return Winner.Player;
            }
            return Winner.CPU;
        }
    }
}