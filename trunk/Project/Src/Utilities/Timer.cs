using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Timer
    {
        private static Timer _mSingleton = null;
        private float _mTime = 0.0f;

        //-------------------------------------------------------------------------
        private Timer()
        {
        }

        public void Initialize()
        {
            _mTime = 0.0f;
        }

        public float Time
        {
            get { return _mTime; }
        }

        //-------------------------------------------------------------------------
        public static Timer Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new Timer();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            _mTime += _Dt;
        }
    }
}