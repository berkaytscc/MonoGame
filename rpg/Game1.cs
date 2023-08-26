using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Comora;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;

namespace rpg
{
    enum Direction
    {
        Down,
        Up,
        Left,
        Right
    }

    public class Game1 : Game
    {
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Texture2D _playerSprite;
        private Texture2D _walkDown;
        private Texture2D _walkUp;
        private Texture2D _walkRight;
        private Texture2D _walkLeft;

        private Texture2D _background;
        private Texture2D _ball;
        private Texture2D _skull;

        private Player _player = new Player();
        private Camera _camera;

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

            this._camera = new Camera(_graphics.GraphicsDevice);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            TryLoadContent();

            _player.animations[0] = new SpriteAnimation(_walkDown, 4, 8);
            _player.animations[1] = new SpriteAnimation(_walkUp, 4, 8);
            _player.animations[2] = new SpriteAnimation(_walkLeft, 4, 8);
            _player.animations[3] = new SpriteAnimation(_walkRight, 4, 8);

            Sound.projectileSound = Content.Load<SoundEffect>("Sounds/blip");
            Sound.bgMusic = Content.Load<Song>("Sounds/nature");
            MediaPlayer.Play(Sound.bgMusic);

            _player.anim = _player.animations[0];
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            _player.Update(gameTime);
            if(!_player.IsDead)
                Controller.Update(gameTime, _skull);

            this._camera.Position = _player.Position;
            this._camera.Update(gameTime);

            foreach (Projectile projectile in Projectile.Projectiles)
            {
                projectile.Update(gameTime);
            }

            foreach (Enemy enemy in Enemy.Enemies)
            {
                enemy.Update(gameTime, _player.Position, _player.IsDead);
                int sum = 32 + enemy.radius;
                if(Vector2.Distance(_player.Position, enemy.Position) < sum)
                {
                    _player.IsDead = true;
                }
            }

            foreach (Projectile projectile in Projectile.Projectiles)
            {
                foreach (Enemy enemy in Enemy.Enemies)
                {
                    int sum = projectile.radius + enemy.radius;
                    if(Vector2.Distance(projectile.Position, enemy.Position) < sum)
                    {
                        projectile.Collided = true;
                        enemy.IsDead = true;
                    }
                }
            }

            Projectile.Projectiles.RemoveAll(p => p.Collided);
            Enemy.Enemies.RemoveAll(e => e.IsDead);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin(this._camera);

            _spriteBatch.Draw(_background, new Vector2(-500, -500), Color.White);

            foreach (Enemy enemy in Enemy.Enemies)
            {
                enemy.anim.Draw(_spriteBatch);
            }

            foreach (Projectile projectile in Projectile.Projectiles)
            {
                _spriteBatch.Draw(_ball, new Vector2(projectile.Position.X - 48, projectile.Position.Y - 48), Color.White);
            }

            if (!_player.IsDead)
            {
                _player.anim.Draw(_spriteBatch);
            }

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        private void TryLoadContent()
        {
            _playerSprite = Content.Load<Texture2D>("Player/player");
            _walkDown = Content.Load<Texture2D>("Player/walkDown");
            _walkUp = Content.Load<Texture2D>("Player/walkUp");
            _walkRight = Content.Load<Texture2D>("Player/walkRight");
            _walkLeft = Content.Load<Texture2D>("Player/walkLeft");

            _background = Content.Load<Texture2D>("background");
            _ball = Content.Load<Texture2D>("ball");
            _skull = Content.Load<Texture2D>("skull");
        }
    }
}