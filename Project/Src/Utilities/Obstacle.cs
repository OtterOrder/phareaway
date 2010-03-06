using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Obstacle
    {
        private Vector2 _mPosition = new Vector2();
        private float   _mSpeed = 0.0f;

        private Sprite _mSprite = null;

        //-------------------------------------------------------------------------
        public Obstacle()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, string _FileName, UInt32 _SceneId, float _Depth, float _Speed, float _X, float _Y)
        {
            _mSprite = SceneManager.Singleton.GetNewSprite( _FileName,
                                                            _ContentManager,
                                                            _SceneId);
            _mSprite.Depth = _Depth;
            _mPosition = new Vector2(_X, _Y);
            _mSprite.mOrigin = new Vector2((float)_mSprite.Width / 2.0f, (float)_mSprite.Height / 2.0f);
            _mSprite.SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSprite.Width, _mSprite.Height));
            _mSprite.mVisible = true;

            _mSpeed = _Speed;
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

        public bool Visible
        {
            get { return _mSprite.mVisible; }
            set { _mSprite.mVisible = value; }
        }
        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            _mPosition.Y += _mSpeed*_Dt;

            _mSprite.mPosition = _mPosition;
        }
    }
}