using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace rpg
{
    internal class Player
    {
        private Vector2 _position = new Vector2(500, 300);
        private int _speed = 300;
        private Direction _playerDirection = Direction.Down;
        private bool _isMoving = false;
        private KeyboardState _previusKState = Keyboard.GetState();
        public bool _isDead = false;

        public SpriteAnimation anim;

        public SpriteAnimation[] animations = new SpriteAnimation[4];

        public Vector2 Position
        {
            get
            {
                return _position;
            }
        }

        public bool IsDead
        {
            get { return _isDead; }
            set { _isDead = value; }
        }

        public void setPlayerX(float newX)
        {
            _position.X = newX;
        }

        public void setPlayerY(float newY)
        {
            _position.Y = newY;
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState kState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            _isMoving = false;

            if(kState.IsKeyDown(Keys.Right) || kState.IsKeyDown(Keys.D))
            {
                _playerDirection = Direction.Right;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Left) || kState.IsKeyDown(Keys.A))
            {
                _playerDirection = Direction.Left;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Up) || kState.IsKeyDown(Keys.W))
            {
                _playerDirection = Direction.Up;
                _isMoving = true;
            }
            if (kState.IsKeyDown(Keys.Down) || kState.IsKeyDown(Keys.S))
            {
                _playerDirection = Direction.Down;
                _isMoving = true;
            }

            if(kState.IsKeyDown(Keys.Space))
            {
                _isMoving = false;
            }

            if(_isDead)
            {
                _isMoving = false;
            }

            if(_isMoving)
            {
                switch (_playerDirection)
                {
                    case Direction.Right:
                        if(_position.X < 1275)
                            _position.X += _speed * dt;
                        break;
                    case Direction.Left:
                        if (_position.X > 225)
                            _position.X -= _speed * dt;
                        break;
                    case Direction.Up:
                        if (_position.Y >200)
                            _position.Y -= _speed * dt;
                        break;
                    case Direction.Down:
                        if (_position.Y < 1250)
                            _position.Y += _speed * dt;
                        break;
                }
            }

            //switch (dir)
            //{
            //    case Direction.Down:
            //        anim = animations[0];
            //        break;
            //    case Direction.Up:
            //        anim = animations[1];
            //        break;
            //    case Direction.Left:
            //        anim = animations[2];
            //        break;
            //    case Direction.Right:
            //        anim = animations[3];
            //        break;
            //}

            anim = animations[(int)_playerDirection];

            anim.Position = new Vector2(_position.X - 48, _position.Y - 48);

            if(kState.IsKeyDown(Keys.Space))
            {
                anim.setFrame(0);
            }
            else if(_isMoving)
            {
                anim.Update(gameTime);
            }
            else
            {
                anim.setFrame(1);
            }

            if(kState.IsKeyDown(Keys.Space) && _previusKState.IsKeyUp(Keys.Space))
            {
                Projectile.Projectiles.Add(new Projectile(_position, _playerDirection));
                Sound.projectileSound.Play(0.5f, 0.5f, 0f);
            }

            _previusKState = kState;
        }
    }
}
