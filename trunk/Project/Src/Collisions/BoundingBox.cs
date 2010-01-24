using Microsoft.Xna.Framework;

namespace PhareAway
{
    public class BoundingBox
    {
        public Vector2 mRelativePostion = Vector2.Zero;
        public Vector2 mSize = Vector2.Zero;

        private Sprite  _mSpr;

        private float _mLeft    = 0.0f;
        private float _mRight   = 0.0f;
        private float _mTop     = 0.0f;
        private float _mBottom  = 0.0f;

        //-------------------------------------------------------------------------
        public BoundingBox(Sprite _Spr, Vector2 _Position, Vector2 _Size)
        {
            mRelativePostion = _Position;
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

        //-------------------------------------------------------------------------
        public void Update ()
        {
            Vector2 Pos = _mSpr._mPosition - _mSpr._mOrigin;

            _mLeft = Pos.X;
            _mRight = Pos.X + mSize.X;

            _mTop = Pos.Y;
            _mBottom = Pos.Y + mSize.Y;
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