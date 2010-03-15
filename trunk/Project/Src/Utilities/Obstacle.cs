using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Obstacle
    {
        public enum State
        {
            Idle,
            Explode,
            Dead
        }

        private Vector2 _mPosition = new Vector2();
        private float   _mSpeed = 0.0f;

        private Sprite _mSprite = null;

        private State _mState = State.Idle;

        public static Sprite ExplsoionSpr = null;

        //-------------------------------------------------------------------------
        public Obstacle()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, string _FileName, UInt32 _SceneId, float _Depth, float _Speed, float _X, float _Y)
        {
            SceneManager.Singleton.RemoveSprite(_mSprite, _SceneId);
            DeleteBoundingBox();

            _mSprite = SceneManager.Singleton.GetNewSprite( _FileName,
                                                            _ContentManager,
                                                            _SceneId);
            _mSprite.Depth = _Depth;
            _mPosition = new Vector2(_X, _Y);
            _mSprite.mOrigin = new Vector2((float)_mSprite.Width / 2.0f, (float)_mSprite.Height / 2.0f);
            _mSprite.SetBoundingBox((UInt32)CollisionId.Obstacle, new Vector2(0.0f, 0.0f), new Vector2(_mSprite.Width, _mSprite.Height));
            _mSprite.mVisible = true;

            _mSpeed = _Speed;

            _mState = State.Idle;
        }

        public void InitExplosion(ContentManager _ContentManager, string _FileName, UInt32 _SceneId, UInt32 _NbFrames, float _Fps)
        {
            SceneManager.Singleton.RemoveSprite(_mSprite, _SceneId);
            DeleteBoundingBox();

            _mSprite = SceneManager.Singleton.GetNewSprite( _FileName,
                                                            _ContentManager,
                                                            _SceneId,
                                                            _NbFrames,
                                                            _Fps);

            _mSprite.mOrigin = new Vector2((float)_mSprite.Width / 2.0f, (float)_mSprite.Height / 2.0f);
            _mSprite.mVisible = true;
            _mSprite.mPosition = _mPosition;
            _mState = State.Explode;
        }

        //------------------------------------------------------------------
        public Vector2 GetPosition() { return _mPosition; }

        public void SetPosition(Vector2 _Position) { _mPosition = _Position; }

        private BoundingBox BBox
        {
            get
            {
                _mSprite.GetBoundingBox().Update();
                return _mSprite.GetBoundingBox();
            }
        }

        public BoundingBox BoundingBox
        {
            get { return _mSprite.GetBoundingBox(); }
        }

        public Sprite Sprite
        {
            get { return _mSprite; }
        }

        public bool Visible
        {
            get { return _mSprite.mVisible; }
            set { _mSprite.mVisible = value; }
        }

        private void DeleteBoundingBox ()
        {
            if (_mSprite != null)
                CollisionsManager.Singleton.DeleteBoundingBox((UInt32)CollisionId.Obstacle, _mSprite.GetBoundingBox());
        }

        public State mState
        {
            get { return _mState; }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            if(_mState == State.Idle || _mState == State.Explode)
            {
                _mPosition.Y += _mSpeed * _Dt;
                _mSprite.mPosition = _mPosition;

                if (_mState == State.Explode && _mSprite.AnimPlayer != null && _mSprite.AnimPlayer.AtEnd)
                {
                    _mState = State.Dead;
                    _mSprite.mVisible = false;
                }
            }
        }
    }
}