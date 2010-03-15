using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class FlashesParameters
    {
        public string mFileBase = "";
        public string mFileName = "";

        public float mDepth = 0.5f;

        public float mSpeed = 1.0f;
        public float mYMin = 0.0f;
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class FlashManager
    {
        private static FlashManager _mSingleton = null;

        private List<Flash> _mFlashList = null;

        private FlashesParameters _mParams;

        private ContentManager _mContentManager = null;
        private UInt32 _mSceneId;

        //-------------------------------------------------------------------------
        private FlashManager()
        {
        }

        public void Initialize(FlashesParameters _Params, ContentManager _ContentManager, UInt32 _SceneId)
        {
            _mParams = _Params;

            _mFlashList = new List<Flash>();

            _mContentManager = _ContentManager;
            _mSceneId = _SceneId;
        }

        //-------------------------------------------------------------------------
        public static FlashManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new FlashManager();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            for (int i = _mFlashList.Count - 1; i >= 0; i--)
            {
                _mFlashList[i].Update(_Dt);

                if (_mFlashList[i].GetPosition().Y <= _mParams.mYMin)
                    FreeFlash(i);
                else
                {
                    if(ObstacleManager.Singleton.ExplodeObstacle( CollisionsManager.Singleton.Collide(_mFlashList[i].Sprite, (int)CollisionId.Obstacle, Vector2.Zero, true)))
                        FreeFlash(i);
                }
            }
        }

        //-----------------------------------
        private void FreeFlash(int _Idx)
        {
            SceneManager.Singleton.RemoveSprite(_mFlashList[_Idx].Sprite, _mSceneId);
            _mFlashList.RemoveAt(_Idx);
        }

        //-----------------------------------
        public void CreateFlash(float _X, float _Y)
        {
            _mFlashList.Add(new Flash(_mContentManager, _mParams.mFileBase + _mParams.mFileName, _mSceneId, _mParams.mDepth, _mParams.mSpeed, _X, _Y));
        }
    }
}