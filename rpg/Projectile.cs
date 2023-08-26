using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace rpg
{
    internal class Projectile
    {
        public static List<Projectile> Projectiles = new List<Projectile>();

        private Vector2 _position;
        private int _speed = 1000;
        public int radius = 18;
        private Direction dir;

        private bool _collided = false;

        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public bool Collided
        {
            get { return _collided; }
            set { _collided = value; }
        }

        public Projectile(Vector2 newPos, Direction dir)
        {
            _position = newPos;
            this.dir = dir;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            switch (dir)
            {
                case Direction.Right:
                    _position.X += _speed * dt;
                    break;
                case Direction.Left:
                    _position.X -= _speed * dt;
                    break;
                case Direction.Up:
                    _position.Y -= _speed * dt;
                    break;
                case Direction.Down:
                    _position.Y += _speed * dt;
                    break;
            }
        }

    }
}
