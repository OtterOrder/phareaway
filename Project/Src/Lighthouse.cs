using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class LGameParameters
    {
        public float mXSpeed = 0.1f;
        public float mYSpeed = 0.1f;

        public float mXMin = 0.0f;
        public float mXMax = 0.0f;
        public float mYMin = 0.0f;
        public float mYMax = 0.0f;
    }

    public class LighthouseParameters
    {
        public string mFileBase = "";
        public SpriteParameters mSprLightouse = new SpriteParameters();
        public SpriteParameters mSprJetEngine = new SpriteParameters();
        public LGameParameters mGameParams = new LGameParameters();
        public float mDepth = 0.5f;
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Lighthouse
    {
        public bool mActive = false;

        private InputParameters _mInputParams = null;
        private LGameParameters _mGameParams = null;

        private Sprite   _mSprLighthouse = null;
        private Sprite[] _mSprJetEngine = new Sprite[2];

        private Vector2 _mPosition = new Vector2();

        private Vector2 _mSpeed = new Vector2();

        //-------------------------------------------------------------------------
        public Lighthouse()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, LighthouseParameters _Parameters, UInt32 _SceneId)
        {
            _mGameParams = _Parameters.mGameParams;

            _mSprLighthouse = LoadSprite(_ContentManager, _SceneId, _Parameters.mFileBase, _Parameters.mSprLightouse);
            _mSprLighthouse.mOrigin = new Vector2((float)_mSprLighthouse.Width / 2.0f, (float)_mSprLighthouse.Height);
            _mSprLighthouse.Depth = _Parameters.mDepth;
            _mSprLighthouse.SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSprLighthouse.Width, _mSprLighthouse.Height));

            _mSprJetEngine[0] = LoadSprite(_ContentManager, _SceneId, _Parameters.mFileBase, _Parameters.mSprJetEngine);
            _mSprJetEngine[1] = LoadSprite(_ContentManager, _SceneId, _Parameters.mFileBase, _Parameters.mSprJetEngine);

            for (int i = 0; i < 2; i++)
            {
                _mSprJetEngine[i].mOrigin = new Vector2((float)_mSprJetEngine[i].Width / 2.0f, (float)0.0f);
                _mSprJetEngine[i].Depth = _Parameters.mDepth;
                //_mSprJetEngine[i].SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSprJetEngine[i].Width, _mSprJetEngine[i].Height));
            }
        }

        private Sprite LoadSprite (ContentManager _ContentManager, UInt32 _SceneId, string _FileBase, SpriteParameters _SprParams)
        {
            Sprite spr = SceneManager.Singleton.GetNewSprite(   _FileBase + _SprParams.mFileName,
                                                                _ContentManager,
                                                                _SceneId,
                                                                _SprParams.mNbFrames,
                                                                _SprParams.mFps);
            if (spr.AnimPlayer != null)
                spr.AnimPlayer.Loop = _SprParams.mLoop;

            return spr;
        }

        //------------------------------------------------------------------
        public Vector2 GetPosition() { return _mPosition; }

        public void SetPosition(Vector2 _Position) { _mPosition = _Position; }

        public InputParameters InputParameters
        {
            set { _mInputParams = value; }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            _mSpeed = Vector2.Zero;

            if (mActive)
            {
                UpdateInputs(_Dt);
            }

            _mPosition += _mSpeed * _Dt;

            _mPosition.X = MathHelper.Clamp(_mPosition.X, _mGameParams.mXMin, _mGameParams.mXMax);
            _mPosition.Y = MathHelper.Clamp(_mPosition.Y, _mGameParams.mYMin, _mGameParams.mYMax);

            _mSprLighthouse.mPosition = _mPosition;


            _mSprJetEngine[0].mPosition = new Vector2(_mPosition.X - 9.0f, _mPosition.Y +1.0f);
            _mSprJetEngine[1].mPosition = new Vector2(_mPosition.X + 8.0f, _mPosition.Y +1.0f);
        }

        //-----------------------------------
        private void UpdateInputs(float _Dt)
        {
            if (_mInputParams == null)
                return;

            _mSpeed.X = 0.0f;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mRight))
                _mSpeed.X = _mGameParams.mXSpeed;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mLeft))
                _mSpeed.X = -_mGameParams.mXSpeed;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mDown))
                _mSpeed.Y = _mGameParams.mYSpeed;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mUp))
                _mSpeed.Y = -_mGameParams.mYSpeed;
        }
    }
}