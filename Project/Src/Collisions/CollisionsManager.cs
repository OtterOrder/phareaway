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

        //-------------------------------------------------------------------------
        public BoundingBox Collide(Sprite _Spr, UInt32 _Type, Vector2 _Offset)
        {
            if (_Spr.GetBoundingBox() == null)
                return null;

            BBoxList lList = null;

            // Find the list
            IEnumerator<BBoxList> ItBBoxList = _mBBoxLists.GetEnumerator();
            ItBBoxList.Reset();
            while (ItBBoxList.MoveNext())
            {
                if (ItBBoxList.Current.mType == _Type)
                {
                    lList = ItBBoxList.Current;
                    break;
                }
            }

            if (lList == null)
                return null;

            BoundingBox BBoxSpr = new BoundingBox(_Spr, _Spr.GetBoundingBox().mRelativePostion + _Offset, _Spr.GetBoundingBox().mSize);

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
    }
}