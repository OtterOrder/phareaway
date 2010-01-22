using System;

namespace PhareAway
{

    class AnimationPlayer
    {
        private UInt32 _mNbFrames = 0;

        private float _mFps = 0.0f;
        private float _mTime = 0.0f;

        private float _mSpeed = 1.0f;
        private int _mState = 0;

        private int _mCurrentFrame = 0;

        enum AnimState
        {
            Pause = 1,
            Loop = 1 << 1
        }

        //-------------------------------------------------------------------------
        public AnimationPlayer(UInt32 _NbFrames, float _Fps)
        {
            _mNbFrames = _NbFrames;
            _mFps = _Fps;
        }

        //-------------------------------------------------------------------------
        public UInt32 NbFrames
        {
            get { return _mNbFrames; }
        }

        public float Fps
        {
            get { return _mFps; }
        }

        public float Speed
        {
            get { return _mSpeed; }
            set { _mSpeed = value; }
        }

        public bool Loop
        {
            get { return ((_mState & (int)AnimationPlayer.AnimState.Loop) == (int)AnimationPlayer.AnimState.Loop); }

            set
            {
                if (value)
                    _mState = _mState | (int)AnimationPlayer.AnimState.Loop;
                else
                    _mState = _mState & ~(int)AnimationPlayer.AnimState.Loop;
            }
        }

        public bool Pause
        {
            get { return ((_mState & (int)AnimationPlayer.AnimState.Pause) == (int)AnimationPlayer.AnimState.Pause); }

            set
            {
                if (value)
                    _mState = _mState | (int)AnimationPlayer.AnimState.Pause;
                else
                    _mState = _mState & ~(int)AnimationPlayer.AnimState.Pause;
            }
        }

        public int CurrentFrame
        {
            get { return _mCurrentFrame; }
            set { _mCurrentFrame = Math.Min(Math.Max(value, 0), (int)_mNbFrames - 1); Pause = true; }
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // Seconds
        {
            if (Pause)
                return;

            _mTime += _Dt * _mSpeed;

            _mCurrentFrame = (int)(_mTime * _mFps);

            if (_mCurrentFrame >= _mNbFrames)
            {
                if (Loop)
                {
                    _mTime = 0.0f;
                    _mCurrentFrame = 0;
                }
                else
                {
                    _mCurrentFrame = (int)(_mNbFrames - 1);
                }
            }
        }
    }
}