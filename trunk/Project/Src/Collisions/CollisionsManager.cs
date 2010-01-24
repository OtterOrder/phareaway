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
        /*
        //-------------------------------------------------------------------------
        public void SortSprites()
        {
            _mSprList.Sort(new SpriteComparer());
        }

        public void SortBackgrounds()
        {
            _mBgList.Sort(new BackgroundComparer());
        }
        */
        //-------------------------------------------------------------------------
        public BoundingBox Collide(Sprite _Spr, UInt32 _Type)
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

            BoundingBox BBoxSpr = _Spr.GetBoundingBox();

            lList.Update();
            BBoxSpr.Update();


            IEnumerator<BoundingBox> ItBBox = lList.mList.GetEnumerator();
            ItBBox.Reset();
            while (ItBBox.MoveNext())
            {
                if (   BBoxSpr.Left >= ItBBox.Current.Left && BBoxSpr.Left <= ItBBox.Current.Right      // Spr Left-Top corner
                    && BBoxSpr.Top  >= ItBBox.Current.Top  && BBoxSpr.Top  <= ItBBox.Current.Bottom)
                {
                    return ItBBox.Current;
                }
                else
                if (   BBoxSpr.Right >= ItBBox.Current.Left && BBoxSpr.Right <= ItBBox.Current.Right    // Spr Right-Top corner
                    && BBoxSpr.Top   >= ItBBox.Current.Top && BBoxSpr.Top    <= ItBBox.Current.Bottom)
                {
                    return ItBBox.Current;
                }
                else
                if (   BBoxSpr.Left   >= ItBBox.Current.Left && BBoxSpr.Left   <= ItBBox.Current.Right  // Spr Left-Bottom corner
                    && BBoxSpr.Bottom >= ItBBox.Current.Top  && BBoxSpr.Bottom <= ItBBox.Current.Bottom)
                {
                    return ItBBox.Current;
                }
                else
                if (   BBoxSpr.Right  >= ItBBox.Current.Left && BBoxSpr.Right  <= ItBBox.Current.Right  // Spr Right-Bottom corner
                    && BBoxSpr.Bottom >= ItBBox.Current.Top  && BBoxSpr.Bottom <= ItBBox.Current.Bottom)
                {
                    return ItBBox.Current;
                }
                if (   ItBBox.Current.Left >= BBoxSpr.Left && ItBBox.Current.Left <= BBoxSpr.Right      // Left-Top corner
                    && ItBBox.Current.Top  >= BBoxSpr.Top  && ItBBox.Current.Top  <= BBoxSpr.Bottom)
                {
                    return ItBBox.Current;
                }
                else
                if (   ItBBox.Current.Right >= BBoxSpr.Left && ItBBox.Current.Right <= BBoxSpr.Right    // Right-Top corner
                    && ItBBox.Current.Top   >= BBoxSpr.Top  && ItBBox.Current.Top   <= BBoxSpr.Bottom)
                {
                    return BBoxSpr;
                }
                else
                if (   ItBBox.Current.Left   >= BBoxSpr.Left && ItBBox.Current.Left   <= BBoxSpr.Right  // Left-Bottom corner
                    && ItBBox.Current.Bottom >= BBoxSpr.Top  && ItBBox.Current.Bottom <= BBoxSpr.Bottom)
                {
                    return BBoxSpr;
                }
                else
                if (   ItBBox.Current.Right  >= BBoxSpr.Left && ItBBox.Current.Right  <= BBoxSpr.Right  // Right-Bottom corner
                    && ItBBox.Current.Bottom >= BBoxSpr.Top  && ItBBox.Current.Bottom <= BBoxSpr.Bottom)
                {
                    return BBoxSpr;
                }
            }

            return null;
        }
    }
}