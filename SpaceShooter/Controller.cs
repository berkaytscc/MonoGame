using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Threading;

namespace SpaceShooter
{
    internal class Controller
    {
        public double _timer = 2;
        private double _maxTime = 2;
        private double _totalTime = 0f;
        private int _speed = 220;
        public int _nextSpeed = 240;
        private int _countdownTimer = 3;
        private float _asteroidSpawnTreshold = 0.5f;
        private int _asteroidSpeedTreshold = 720;
        private bool _inMainMenu = true;
        private bool _inGame = false;

        public double TotalTime => _totalTime;

        public bool InMainMenu
        {
            get
            {
                return _inMainMenu;
            }

            set
            {
                _inMainMenu = value;
            }
        }

        public bool InGame
        {
            get
            {
                return _inGame;
            }

            set
            {
                _inGame = value;
            }
        }

        private List<Asteroid> _asteroids = new List<Asteroid>();
        public List<Asteroid> Asteroids => _asteroids;

        public void ControlUpdate(GameTime gameTime)
        {
            if (InGame)
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
                _totalTime += gameTime.ElapsedGameTime.TotalSeconds;
            }
            else
            {
                KeyboardState kState = Keyboard.GetState();
                if (kState.IsKeyDown(Keys.Enter))
                {
                    _inGame = true;
                    _inMainMenu = false;
                    _totalTime = 0f;
                    _timer = 2D;
                    _maxTime = 2D;
                    _nextSpeed = 240;
                }
            }

            if (_timer <= 0)
            {
                _asteroids.Add(new Asteroid(_nextSpeed));
                _timer = _maxTime;
                if (_maxTime > 0.5)
                {
                    _maxTime -= 0.1D;
                }
                if (_nextSpeed < 720)
                {
                    _nextSpeed += 4;
                }
            }
        }
    }
}
