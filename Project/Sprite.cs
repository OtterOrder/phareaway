using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace PhareAway
{
    class Sprite
    {
        private Texture2D       _mSpr = null;
        private AnimationPlayer _mAnimPlayer = null;
        private int             _mWidth;

        public Vector2          _mPosition;
        public float            _mDepth = 1.0f;

        //-------------------------------------------------------------------------
        public Sprite(string _FileName, ContentManager _ContentManager)
        {
            _mSpr = _ContentManager.Load<Texture2D>(_FileName);
            _mWidth = _mSpr.Width;
        }

        public Sprite(string _FileName, ContentManager _ContentManager, UInt32 _NbFrames, float _Fps)
        {
            _mSpr = _ContentManager.Load<Texture2D>(_FileName);

            _mAnimPlayer = new AnimationPlayer(_NbFrames, _Fps);
            _mWidth = _mSpr.Width / (int)_NbFrames;
        }

        //-------------------------------------------------------------------------
        public int Height
        {
            get { return _mSpr.Height; }
        }

        public int Width
        {
            get { return _mWidth; }
        }

        public AnimationPlayer AnimPlayer
        {
            get { return _mAnimPlayer; }
        }

        //-------------------------------------------------------------------------
        public void Update (float _Dt)  // Seconds
        {
            if (_mAnimPlayer != null)
                _mAnimPlayer.Update(_Dt);
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch)
        {
            int Frame = 0;

            if (_mAnimPlayer != null)
                Frame = _mAnimPlayer.CurrentFrame;

            Rectangle Rect = new Rectangle(Frame*_mWidth, 0, _mWidth, Height);

            _SprBatch.Draw(_mSpr, _mPosition, Rect, Color.White, 0, Vector2.Zero, 4.0f, SpriteEffects.None, _mDepth);
        }
    }
}