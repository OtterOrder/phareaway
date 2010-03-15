using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace PhareAway
{
    public class BBoxList 
    {
        public UInt32 mType = 0;
        public List<BoundingBox> mList = new List<BoundingBox>();

        //-------------------------------------------------------------------------
        public void Update()
        {
            IEnumerator<BoundingBox> It = mList.GetEnumerator();
            It.Reset();
            while (It.MoveNext())
            {
                It.Current.Update();
            }
        }

        public void DeletBoundingBox (BoundingBox _BBox)
        {
            mList.Remove(_BBox);
        }
    }

    public class CollisionsManager
    {
        private static CollisionsManager _mSingleton = null;
        private List<BBoxList> _mBBoxLists = new List<BBoxList>();

        //-------------------------------------------------------------------------
        private CollisionsManager()
        {
        }

        //-------------------------------------------------------------------------
        public static CollisionsManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new CollisionsManager();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        public BoundingBox GetNewBoundingBox(UInt32 _Type, Sprite _Spr, Vector2 _Position, Vector2 _Size)
        {
            BoundingBox BBox = new BoundingBox(_Spr, _Position, _Size);

            IEnumerator<BBoxList> ItBBoxList = _mBBoxLists.GetEnumerator();
            ItBBoxList.Reset();
            while (ItBBoxList.MoveNext())
            {
                if (ItBBoxList.Current.mType == _Type)
                {
                    ItBBoxList.Current.mList.Add(BBox);
                    return BBox;
                }
            }

            BBoxList lList = new BBoxList();
            lList.mType = _Type;
            lList.mList.Add(BBox);
            _mBBoxLists.Add(lList);

            return BBox;
        }

        public void DeleteBoundingBox (UInt32 _Type, BoundingBox _BBox)
        {
            GetList(_Type).DeletBoundingBox(_BBox);
        }

        //-------------------------------------------------------------------------
        private BBoxList GetList(UInt32 _Type)
        {
            IEnumerator<BBoxList> ItBBoxList = _mBBoxLists.GetEnumerator();
            ItBBoxList.Reset();
            while (ItBBoxList.MoveNext())
            {
                if (ItBBoxList.Current.mType == _Type)
                {
                    return ItBBoxList.Current;
                }
            }

            return null;
        }

        //-------------------------------------------------------------------------
        public BoundingBox Collide(Sprite _Spr, UInt32 _Type, Vector2 _Offset)
        {
            if (_Spr.GetBoundingBox() == null)
                return null;

            BBoxList lList = GetList(_Type);

            if (lList == null)
                return null;

            _Spr.GetBoundingBox().Update();
            BoundingBox BBoxSpr = new BoundingBox(_Spr, _Spr.GetBoundingBox().mPostion + _Offset, _Spr.GetBoundingBox().mSize);

            lList.Update();
            BBoxSpr.Update();

            IEnumerator<BoundingBox> ItBBox = lList.mList.GetEnumerator();
            ItBBox.Reset();
            while (ItBBox.MoveNext())
            {
                if (ItBBox.Current.Collide(BBoxSpr))
                    return ItBBox.Current;
            }

            return null;
        }

        //-------------------------------------------------------------------------
        public BoundingBox Collide(Sprite _Spr, UInt32 _Type, Vector2 _Offset, bool Precise)
        {
            if (_Spr.GetBoundingBox() == null)
                return null;

            BBoxList lList = GetList(_Type);

            if (lList == null)
                return null;

            _Spr.GetBoundingBox().Update();
            BoundingBox BBoxSpr = new BoundingBox(_Spr, _Spr.GetBoundingBox().mPostion + _Offset, _Spr.GetBoundingBox().mSize);

            lList.Update();
            BBoxSpr.Update();

            IEnumerator<BoundingBox> ItBBox = lList.mList.GetEnumerator();
            ItBBox.Reset();

            while (ItBBox.MoveNext())
            {
                if (ItBBox.Current.Collide(BBoxSpr, Precise))
                    return ItBBox.Current;
            }

            return null;
        }

        //-------------------------------------------------------------------------
        public BoundingBox Collide(Vector2 _Point, UInt32 _Type)
        {
            if (_Point == null)
                return null;

            BBoxList lList = GetList(_Type);

            if (lList == null)
                return null;

            lList.Update();

            IEnumerator<BoundingBox> ItBBox = lList.mList.GetEnumerator();
            ItBBox.Reset();
            while (ItBBox.MoveNext())
            {
                if (ItBBox.Current.Collide(_Point))
                    return ItBBox.Current;
            }

            return null;
        }


        //-------------------------------------------------------------------------
        public BoundingBox CollideWithHLine(float _X1, float _X2, float _Y, UInt32 _Type)
        {
            BBoxList lList = GetList(_Type);

            if (lList == null)
                return null;

            lList.Update();

            IEnumerator<BoundingBox> ItBBox = lList.mList.GetEnumerator();
            ItBBox.Reset();
            while (ItBBox.MoveNext())
            {
                if (ItBBox.Current.CollideWithHLine(_X1, _X2, _Y))
                    return ItBBox.Current;
            }

            return null;
        }
    }
}