using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace rpg
{
    internal class Enemy
    {
        public static List<Enemy> Enemies = new List<Enemy>();

        private Vector2 _position = new Vector2(0, 0);
        private int _speed = 150;
        public SpriteAnimation anim;
        public int radius = 30;
        private bool _isDead = false;

        public Enemy( Vector2 newPos, Texture2D spriteSheet)
        {
            _position = newPos;
            anim = new SpriteAnimation(spriteSheet, 10, 6);
        }

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

        public void Update(GameTime gameTime, Vector2 playerPos, bool isPLayerDead)
        {
            anim.Position = new Vector2(_position.X - 48, _position.Y - 66);
            anim.Update(gameTime);

            if (!isPLayerDead)
            {
                float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;

                Vector2 moveDir = playerPos - _position;
                moveDir.Normalize();
                _position += moveDir * dt * _speed;
            }
        }
    }
}
