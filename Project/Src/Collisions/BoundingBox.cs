using Microsoft.Xna.Framework;

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
            return Contains(_Point.X, _Point.Y);
        }

        public bool CollideWithHLine (float _X1, float _X2, float _Y)
        {
            return (_X1 <= Left && _X2 >= Right && _Y >= Top && _Y <= Bottom);
        }

        public bool CollideWithVLine(float _Y1, float _Y2, float _X)
        {
            return (_Y1 <= Bottom && _Y2 >= Top && _X >= Right && _X <= Left);
        }

        public bool Collide (BoundingBox _BBox)
        {
            if (Contains(_BBox.Left, _BBox.Top)
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
    }

    /*
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
    }*/
}