using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PhareAway
{
    class Background
    {
        private Texture2D   _mBg = null;

        public Vector2      _mScale = new Vector2(1.0f, 1.0f);
        public float        _mDepth = 1.0f;

        public  Vector2     _mSpeed;
        private Vector2     _mPosition;

        //-------------------------------------------------------------------------
        public Background(string _FileName, ContentManager _ContentManager)
        {
            _mBg = _ContentManager.Load<Texture2D>(_FileName);
        }

        //-------------------------------------------------------------------------
        public int Height
        {
            get { return _mBg.Height; }
        }

        public int Width
        {
            get { return _mBg.Width; }
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // Seconds
        {
            _mPosition += _mSpeed * _Dt;
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch)
        {
            Rectangle Rect = new Rectangle((int)_mPosition.X, (int)_mPosition.Y, PhareAwayGame.BackBufferWidth, PhareAwayGame.BackBufferHeight);

            _SprBatch.Draw(_mBg, Vector2.Zero, Rect, Color.White, 0, Vector2.Zero, _mScale, SpriteEffects.None, _mDepth);
        }
    }
}