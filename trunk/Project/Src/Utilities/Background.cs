using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhareAway
{
    public class Background
    {
        private Texture2D   _mBg = null;

        public Vector2      _mScale = new Vector2(1.0f, 1.0f);
        private float       _mDepth = 1.0f;

        public Vector2      _mSpeed = Vector2.Zero;
        private Vector2     _mPosition = Vector2.Zero;

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

        public float Depth
        {
            get { return _mDepth; }
            set { _mDepth = value; SceneManager.Singleton.SortBackgrounds(); }
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            _mPosition += _mSpeed * _Dt;
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch)
        {
            Rectangle Rect = new Rectangle((int)_mPosition.X, (int)_mPosition.Y, PhareAwayGame.mBackBufferWidth, PhareAwayGame.mBackBufferHeight);

            _SprBatch.Draw(_mBg, Vector2.Zero, Rect, Color.White, 0, Vector2.Zero, _mScale, SpriteEffects.None, _mDepth);
        }
    }

    //-------------------------------------------------------------------------
    public class BackgroundComparer : IComparer<Background>
    {
        public int Compare(Background _Bg1, Background _Bg2)
        {
            if (_Bg1.Depth > _Bg2.Depth)
                return -1;
            else
            if (_Bg1.Depth < _Bg2.Depth)
                return 1;

            return 0;
        }
    }
}