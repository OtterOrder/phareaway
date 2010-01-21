using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Text;

namespace PhareAway
{
    class Animation
    {
        private Texture2D[]     _mpFrames   = null;
        private UInt32          _mNbFrames  = 0;

        private float           _mFps       = 0.0f;
        private float           _mTime      = 0.0f;

        private float           _mSpeed     = 1.0f;
        private bool            _mLoop      = false;

        private int             _mCurrentFrame = 0;

        //-------------------------------------------------------------------------
        public Animation (string _FileBase, UInt32 _NbFrames, float _Fps, ContentManager _ContentManager)
        {
            _mpFrames   = new Texture2D [_NbFrames];

            _mNbFrames  = _NbFrames;
            _mFps       = _Fps;

            string FileName;
            for (UInt32 i = 0; i < _mNbFrames; i++)
            {
                FileName = _FileBase;
                FileName += i;

                _mpFrames[i] = _ContentManager.Load<Texture2D>(FileName);
            }
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
            get { return _mLoop; }
            set { _mLoop = value; }
        }

        public int Height
        {
            get { return _mpFrames[0].Height; }
        }

        public int Width
        {
            get { return _mpFrames[0].Width; }
        }

        public Texture2D CurrentFrame
        {
            get { return _mpFrames[_mCurrentFrame]; }
        }

        //-------------------------------------------------------------------------
        public void Update (float _Dt)  // Seconds
        {
            _mTime += _Dt * _mSpeed;

            _mCurrentFrame = (int)(_mTime * _mFps);

            if(_mCurrentFrame >= _mNbFrames)
            {
                if(_mLoop)
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