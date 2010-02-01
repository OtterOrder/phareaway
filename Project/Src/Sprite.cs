using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhareAway
{
    public class Sprite
    {
        private Texture2D       _mSpr = null;
        private AnimationPlayer _mAnimPlayer = null;
        private int             _mWidth;

        public Vector2          mPosition = Vector2.Zero;
        public Vector2          mOrigin = Vector2.Zero;
        public Vector2          mScale = new Vector2(1.0f, 1.0f);
        private float           _mDepth = 1.0f;
        public SpriteEffects    mFlip = SpriteEffects.None;

        private BoundingBox     _mBBox = null;

        public bool             mVisible = true;

        //-------------------------------------------------------------------------
        public Sprite(string _FileName, ContentManager _ContentManager)
        {
            _mSpr = _ContentManager.Load<Texture2D>(_FileName);
            _mWidth = _mSpr.Width;
        }

        public Sprite(string _FileName, ContentManager _ContentManager, UInt32 _NbFrames, float _Fps)
        {
            _mSpr = _ContentManager.Load<Texture2D>(_FileName);

            if (_NbFrames > 1)
            {
                _mAnimPlayer = new AnimationPlayer(_NbFrames, _Fps);
                _mWidth = _mSpr.Width / (int)_NbFrames;
            }
            else
                _mWidth = _mSpr.Width;
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

        public float Depth
        {
            get { return _mDepth; }
            set { _mDepth = value; SceneManager.Singleton.SortSprites(); }
        }

        public void SetBoundingBox(UInt32 _Type, Vector2 _Position, Vector2 _Size)
        {
            if(_mBBox == null)
                _mBBox = CollisionsManager.Singleton.GetNewBoundingBox(_Type, this, _Position, _Size);
            else
            {
                _mBBox.mRelativePostion = _Position;
                _mBBox.mSize = _Size;
            }
        }

        public BoundingBox GetBoundingBox()
        {
            return _mBBox;
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            if (_mAnimPlayer != null)
                _mAnimPlayer.Update(_Dt / 1000.0f);
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch)
        {
            int Frame = 0;

            if (_mAnimPlayer != null)
                Frame = _mAnimPlayer.CurrentFrame;

            Rectangle Rect = new Rectangle(Frame*_mWidth, 0, _mWidth, Height);

            _SprBatch.Draw(_mSpr, mPosition, Rect, Color.White, 0, mOrigin, mScale, mFlip, _mDepth);
        }
    }

    //-------------------------------------------------------------------------
    public class SpriteComparer : IComparer<Sprite>
    {
        public int Compare(Sprite _Spr1, Sprite _Spr2)
        {
            if (_Spr1.Depth > _Spr2.Depth)
                return -1;
            else
            if (_Spr1.Depth < _Spr2.Depth)
                return 1;

            return 0;
        }
    }
}