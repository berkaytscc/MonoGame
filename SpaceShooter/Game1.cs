using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Reflection.Metadata;

namespace SpaceShooter
{
    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _shipSprite;
        private Texture2D _asteroidSprite;
        private Texture2D _spaceSprite;

        private SpriteFont _gameFont;
        private SpriteFont _timerFont;

        private Ship _ship = new Ship();
        private Controller _gameController = new Controller();

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _graphics.PreferredBackBufferWidth = 1280;
            _graphics.PreferredBackBufferHeight = 720;

            _graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            LoadSprites();
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_gameController.InGame) _ship.Move(gameTime);

            _gameController.ControlUpdate(gameTime);

            for (int i = 0; i < _gameController.Asteroids.Count; i++)
            {
                _gameController.Asteroids[i].Move(gameTime);

                int dist = _gameController.Asteroids[i].Radius + _ship.Radius;
                if (Vector2.Distance(_gameController.Asteroids[i].Position, _ship.Position) < dist) // collision check
                {
                    _gameController.InGame = false;
                    _gameController.InMainMenu = true;

                    _ship.Position = _ship._defaultPosition;
                    _gameController.Asteroids.Clear();
                }
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();   // begin

            _spriteBatch.Draw(_spaceSprite, new Vector2(0, 0), Color.White);
            _spriteBatch.Draw(_shipSprite, new Vector2(_ship.Position.X - 34, _ship.Position.Y - 50), Color.White);

            if(_gameController.InMainMenu)
            {
                DrawStringAtCenter("Press Enter to Begin!");
            }

            for (int i = 0; i < _gameController.Asteroids.Count; i++)
            {
                Vector2 tempPos = _gameController.Asteroids[i].Position;
                int tempRadius = _gameController.Asteroids[i].Radius;
                _spriteBatch.Draw(_asteroidSprite, new Vector2(tempPos.X - tempRadius, tempPos.Y - tempRadius), Color.White);
            }

            _spriteBatch.DrawString(_timerFont, "Time: " + Math.Floor(_gameController.TotalTime).ToString(), new Vector2(3, 3), Color.White);

            _spriteBatch.End();     // end

            base.Draw(gameTime);
        }

        private void LoadSprites()
        {
            _shipSprite = Content.Load<Texture2D>("ship");
            _asteroidSprite = Content.Load<Texture2D>("asteroid");
            _spaceSprite = Content.Load<Texture2D>("space");

            _gameFont = Content.Load<SpriteFont>("spaceFont");
            _timerFont = Content.Load<SpriteFont>("timerFont");
        }

        private void DrawStringAtCenter(string content)
        {
            string contentUpdate = $"{content}";
            var textSize = _gameFont.MeasureString(contentUpdate);
            int center = _graphics.PreferredBackBufferWidth / 2;
            _spriteBatch.DrawString(_gameFont, contentUpdate, new Vector2(center - (textSize.X / 2), 200), Color.White);
        }
    }
}