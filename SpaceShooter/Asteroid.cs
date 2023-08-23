using Microsoft.Xna.Framework;
using System;

namespace SpaceShooter
{
    internal class Asteroid
    {
        private Vector2 _position = new Vector2(600, 300);
        private int _speed = 220;   // initial speed for level 1
        private int _radius = 59;
        private int _asteroidImgHeightInPx = 118;
        public Vector2 Position => _position;
        public int Radius => _radius;


        public Asteroid(int speed)
        {
            _speed = speed;
            Random randomY = new Random();
            _position = new Vector2(1380, randomY.Next(0, 720 - _asteroidImgHeightInPx));
        }

        public void Move(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            _position.X -= _speed * dt;
        }
    }
}
