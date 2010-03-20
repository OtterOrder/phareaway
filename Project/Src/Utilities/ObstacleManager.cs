using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class ObstaclesParameters
    {
        public string mFileBase = "";
        public string[] mSprites = null;
        public SpriteParameters mExplosionSpr = new SpriteParameters();
        private int _mNbObstacles = 1;
        private int _mNbSprites = 1;
        public float mDepth = 0.5f;

        public float mSpeedA = 1.0f;
        public float mSpeedB = 0.0f;

        public float mObstacleProbaA = 1.0f;
        public float mObstacleProbaB = 0.0f;

        public float mStartTimeAMin = 0.0f;
        public float mStartTimeAMax = 0.0f;
        public float mStartTimeB = 0.0f;

        public float mEndTimeAMin = 0.0f;
        public float mEndTimeAMax = 0.0f;
        public float mEndTimeB = 0.0f;

        public Vector2 mXRange = new Vector2(0.0f, 100.0f);
        public float mYInit = 0.0f;

        public float mYMax = 100.0f;

        public ObstaclesParameters(UInt32 _NbObstacles, UInt32 _NbSprites)
        {
            _mNbObstacles = (int)_NbObstacles;
            _mNbSprites = (int)_NbSprites;
            mSprites = new string[_NbSprites];
        }

        public int NbObstacles
        {
            get { return _mNbObstacles; }
        }

        public int NbSprites
        {
            get { return _mNbSprites; }
        }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class ObstacleManager
    {
        public enum State
        {
            Wait = 0,
            LaunchObstacles = 1
        }

        private static ObstacleManager _mSingleton = null;

        private List<Obstacle>  _mObstacleList = null;
        private int             _mNbActiveObstacles = 0;

        private ObstaclesParameters _mParams;
        private Random _mRand = new Random();

        private State _mState = State.LaunchObstacles;

        private float _mTime = 0.0f;
        private float _mNextStateTime;

        private ContentManager _mContentManager = null;
        private UInt32 _mSceneId;

        //-------------------------------------------------------------------------
        private ObstacleManager()
        {
        }

        public void Initialize (ObstaclesParameters _Params, ContentManager _ContentManager, UInt32 _SceneId)
        {
            _mParams = _Params;

            _mObstacleList = new List<Obstacle>(_mParams.NbObstacles);
            _mNbActiveObstacles = 0;

            for (int i = 0; i < _mParams.NbObstacles; i++)
                _mObstacleList.Add(new Obstacle());

            _mContentManager = _ContentManager;
            _mSceneId = _SceneId;

            ChangeState(State.Wait);
        }

        //-------------------------------------------------------------------------
        public static ObstacleManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new ObstacleManager();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            _mTime += _Dt/1000.0f;

            if (_mTime >= _mNextStateTime)
            {
                switch (_mState)
                {
                    case State.Wait:
                        ChangeState(State.LaunchObstacles);
                        break;

                    case State.LaunchObstacles:
                        ChangeState(State.Wait);
                        break;
                }
            }

            switch (_mState)
            {
                case State.Wait:
                    break;

                case State.LaunchObstacles:
                    UpdateLaunchObstacles(_Dt);
                    break;
            }

            for (int i = _mNbActiveObstacles - 1; i >= 0; i--)
            {
                _mObstacleList[i].Update(_Dt);

                if (_mObstacleList[i].GetPosition().Y >= _mParams.mYMax || _mObstacleList[i].mState == Obstacle.State.Dead)
                    FreeObstacle(i);
            }
        }

        //-----------------------------------
        private void ChangeState (State _State)
        {
            if (_State == _mState)
                return;

            switch (_State)
            {
                case State.Wait:
                    _mNextStateTime = (float)_mRand.NextDouble() * (_mParams.mStartTimeAMax - _mParams.mStartTimeAMin) + _mParams.mStartTimeAMin + _mParams.mStartTimeB * Timer.Singleton.Seconds;
                    break;

                case State.LaunchObstacles:
                    _mNextStateTime = (float)_mRand.NextDouble() * (_mParams.mEndTimeAMax - _mParams.mEndTimeAMin) + _mParams.mEndTimeAMin + _mParams.mEndTimeB * Timer.Singleton.Seconds;
                    break;
            }

            _mTime = 0.0f;
            _mState = _State;
        }

        //-----------------------------------
        private void UpdateLaunchObstacles(float _Dt)
        {
            if (_mNbActiveObstacles < _mParams.NbObstacles &&
                _mRand.Next(0, (int)(_mParams.mObstacleProbaA + _mParams.mObstacleProbaB * Timer.Singleton.Seconds) - 1) == 0 &&
                CollisionsManager.Singleton.CollideWithHLine(_mParams.mXRange.X, _mParams.mXRange.Y, _mParams.mYInit, (UInt32)CollisionId.Obstacle) == null)
            {
                _mObstacleList[_mNbActiveObstacles].Init(   _mContentManager,
                                                            _mParams.mFileBase + _mParams.mSprites[_mRand.Next(0, _mParams.NbSprites-1)],
                                                            _mSceneId,
                                                            _mParams.mDepth,
                                                            _mParams.mSpeedA + _mParams.mSpeedB * Timer.Singleton.Seconds,
                                                            _mParams.mXRange.X + (_mParams.mXRange.Y - _mParams.mXRange.X) * (float)_mRand.NextDouble(),
                                                            _mParams.mYInit);
                _mNbActiveObstacles++;
            }
        }

        //-----------------------------------
        private void FreeObstacle(int _Idx)
        {
            Obstacle obs = _mObstacleList[_Idx];
            _mObstacleList.RemoveAt(_Idx);
            _mObstacleList.Add(obs);
            obs.Visible = false;

            _mNbActiveObstacles--;
        }

        public bool ExplodeObstacle (BoundingBox _BBox)
        {
            if (_BBox == null)
                return false;

            for (int i = _mNbActiveObstacles - 1; i >= 0; i--)
            {
                if (_mObstacleList[i].BoundingBox == _BBox)
                {
                    _mObstacleList[i].InitExplosion(_mContentManager, _mParams.mFileBase + _mParams.mExplosionSpr.mFileName, _mSceneId, _mParams.mExplosionSpr.mNbFrames, _mParams.mExplosionSpr.mFps);
                    return true;
                }
            }

            return false;
        }
    }
}