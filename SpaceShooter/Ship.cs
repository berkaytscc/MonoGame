using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace SpaceShooter
{
    internal class Ship
    {
        public Vector2 _defaultPosition = new Vector2(640, 360);
        private Vector2 _position = new Vector2(640, 360);
        private int speed = 180;
        private int _radius = 30;

        public int Radius => _radius;

        public Vector2 Position
        {
            get
            {
                return _position;
            }

            set
            {
                _position = value;
            }
        }

        public Ship()
        {
            SetPositionToDefault();
        }

        public void Move(GameTime gameTime)
        {
            KeyboardState keyboardState = Keyboard.GetState();
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

            if(keyboardState.IsKeyDown(Keys.Right) || keyboardState.IsKeyDown(Keys.D) && _position.X < 1280)
            {
                _position.X += speed * dt;
            }
            
            if(keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.A) && _position.X > 0)
            {
                _position.X -= speed * dt;
            }
            
            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.W) && _position.Y > 0)
            {
                _position.Y -= speed * dt;
            }

            if (keyboardState.IsKeyDown(Keys.Down) || keyboardState.IsKeyDown(Keys.S) && _position.Y < 720)
            {
                _position.Y += speed * dt;
            }
        }

        public void SetPositionToDefault()
        {
            _position = _defaultPosition;
        }
    }
}
