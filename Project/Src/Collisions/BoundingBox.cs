using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace PhareAway
{
    public class BoundingBox
    {
        public Vector2 mPostion = Vector2.Zero;
        public Vector2 mSize = Vector2.Zero;

        private Sprite  _mSpr;

        private float _mLeft    = 0.0f;
        private float _mRight   = 0.0f;
        private float _mTop     = 0.0f;
        private float _mBottom  = 0.0f;

        public bool Active = true;

        //-------------------------------------------------------------------------
        public BoundingBox(Sprite _Spr, Vector2 _Position, Vector2 _Size)
        {
            mPostion = _Position;
            mSize = _Size;

            _mSpr = _Spr;
        }

        //-------------------------------------------------------------------------
        public float Height
        {
            get { return mSize.Y; }
        }

        public float Width
        {
            get { return mSize.X; }
        }

        public float Left
        {
            get { return _mLeft; }
        }

        public float Right
        {
            get { return _mRight; }
        }

        public float Top
        {
            get { return _mTop; }
        }

        public float Bottom
        {
            get { return _mBottom; }
        }

        public Vector2 Size
        {
            get { return mSize * _mSpr.mScale; }
        }

        public Sprite Sprite
        {
            get { return _mSpr; }
        }

        //-------------------------------------------------------------------------
        public void Update ()
        {
            Vector2 Pos = _mSpr.mPosition - _mSpr.mOrigin*_mSpr.mScale + mPostion;

            _mLeft = Pos.X;
            _mRight = Pos.X + mSize.X * _mSpr.mScale.X;

            _mTop = Pos.Y;
            _mBottom = Pos.Y + mSize.Y * _mSpr.mScale.Y;
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public bool Contains(float _X, float _Y)
        {
            if (   _X >= Left && _X <= Right
                && _Y >= Top  && _Y <= Bottom)
            {
                return true;
            }

            return false;
        }

        public bool Collide (Vector2 _Point)
        {
            if (Active)
                return Contains(_Point.X, _Point.Y);
            else
                return false;
        }

        public bool CollideWithHLine (float _X1, float _X2, float _Y)
        {
            if (Active)
                return (_X1 <= Right && _X2 >= Left && _Y >= Top && _Y <= Bottom);
            else
                return false;
        }

        public bool CollideWithVLine(float _Y1, float _Y2, float _X)
        {
            if (Active)
                return (_Y1 <= Bottom && _Y2 >= Top && _X >= Right && _X <= Left);
            else
                return false;
        }

        public bool Collide (BoundingBox _BBox)
        {
            if (!Active)
                return false;

            if (   Contains(_BBox.Left, _BBox.Top)
                || Contains(_BBox.Right, _BBox.Top)
                || Contains(_BBox.Left, _BBox.Bottom)
                || Contains(_BBox.Right, _BBox.Bottom)

                || _BBox.Contains(Left, Top)
                || _BBox.Contains(Right, Top)
                || _BBox.Contains(Left, Bottom)
                || _BBox.Contains(Right, Bottom)

                || CollideWithHLine(_BBox.Left, _BBox.Right, _BBox.Top)
                || CollideWithHLine(_BBox.Left, _BBox.Right, _BBox.Bottom)
                || CollideWithVLine(_BBox.Top, _BBox.Bottom, _BBox.Right)
                || CollideWithVLine(_BBox.Top, _BBox.Bottom, _BBox.Left)

                || _BBox.CollideWithHLine(Left, Right, Top)
                || _BBox.CollideWithHLine(Left, Right, Bottom)
                || _BBox.CollideWithVLine(Top, Bottom, Right)
                || _BBox.CollideWithVLine(Top, Bottom, Left)
                )
                return true;

            return false;
        }

        public bool Collide(BoundingBox _BBox, bool Precise)
        {
            if (!Active)
                return false;

            bool result = Collide(_BBox);

            if(Precise && result)
            {
                Color[] TextData1 = _mSpr.GetData();
                Color[] TextData2 = _BBox.Sprite.GetData();

                Rectangle Rect1 = _mSpr.Rectangle;
                Rectangle Rect2 = _BBox.Sprite.Rectangle;

                int top = Math.Max(Rect1.Top, Rect2.Top);
                int bottom = Math.Min(Rect1.Bottom, Rect2.Bottom);
                int left = Math.Max(Rect1.Left, Rect2.Left);
                int right = Math.Min(Rect1.Right, Rect2.Right);

                for (int y = top; y < bottom; y++)
                {
                    for (int x = left; x < right; x++)
                    {
                        Color color1 = TextData1[(x - Rect1.Left) + (y - Rect1.Top) * Rect1.Width];
                        Color color2 = TextData2[(x - Rect2.Left) + (y - Rect2.Top) * Rect2.Width];

                        if (color1.A != 0 && color2.A != 0)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            return result;
        }
    }
}