using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ShootingTarget
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _targetSprite;
        private Texture2D _crosshairSprite;
        private Texture2D _backgroundSprite;

        private SpriteFont _gameFont;

        private Vector2 _targetPosition = new Vector2(300, 300);    // position will be randomized in the game loop

        private const int _targetRadius = 45;       // 90px width - 90px height for target.png
        private int _score = 0;

        private double _timer = 10;

        MouseState _mouseState;

        private bool _mouseReleased = true;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = false;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadAssets();
        }

        protected override void Update(GameTime gameTime)
        {
            if(Keyboard.GetState().IsKeyDown(Keys.Escape))
            {
                IsMouseVisible = true;
            }

            if(_timer > 0)
            {
                _timer -= gameTime.ElapsedGameTime.TotalSeconds;
            }

            if(_timer < 0)
            {
                _timer = 0;
            }

            _mouseState = Mouse.GetState();

            if (_mouseState.LeftButton == ButtonState.Pressed && _mouseReleased == true)
            {
                IsMouseVisible = false;

                float mouseTargetDistance = Vector2.Distance(_targetPosition, _mouseState.Position.ToVector2());
                if (mouseTargetDistance < _targetRadius && _timer > 0)
                {
                    _score++;

                    Random rand = new Random();

                    _targetPosition.X = rand.Next(0, _graphics.PreferredBackBufferWidth);
                    _targetPosition.Y = rand.Next(0, _graphics.PreferredBackBufferHeight);
                }
                _mouseReleased = false;
            }

            if (_mouseState.LeftButton == ButtonState.Released)
            {
                _mouseReleased = true;
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            HandleSpriteBatch();

            base.Draw(gameTime);
        }

        private void LoadAssets()
        {
            _targetSprite = Content.Load<Texture2D>("target");
            _crosshairSprite = Content.Load<Texture2D>("crosshairs");
            _backgroundSprite = Content.Load<Texture2D>("sky");
            _gameFont = Content.Load<SpriteFont>("galleryFont");
        }

        private void HandleSpriteBatch()
        {
            _spriteBatch.Begin();

            _spriteBatch.Draw(_backgroundSprite, Vector2.Zero, Color.White);
            _spriteBatch.DrawString(_gameFont, $"Score: {_score}", new Vector2(3, 3), Color.White);
            _spriteBatch.DrawString(_gameFont, $"Time: {Math.Ceiling(_timer)}", new Vector2(3, 40), Color.White);
            if (_timer > 0)
            {
                _spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius), Color.White);
            }
            else
            {
                _spriteBatch.Draw(_targetSprite, new Vector2(_targetPosition.X - _targetRadius, _targetPosition.Y - _targetRadius), Color.Gray);
            }

            if(!IsMouseVisible)
            {
                _spriteBatch.Draw(_crosshairSprite, new Vector2(_mouseState.X - 25, _mouseState.Y - 25), Color.White);
            }

            _spriteBatch.End();
        }
    }
}