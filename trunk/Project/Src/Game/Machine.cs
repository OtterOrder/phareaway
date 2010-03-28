using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    public class MachineParameters
    {
        public MachineId mId;
        public SpriteParameters mSprite = new SpriteParameters();
        public float mDepth = 0.5f;

        public Vector2 mBBoxSize;
        public Vector2 mBBoxOffset = Vector2.Zero;

        public Vector2 mPosition;

        public float mHealthMax = 100.0f;
        public float mHealth = 100.0f;

        public float mDamageA = 1.0f;
        public float mDamageB = 0.0f;

        public float mCollisionDamage = 0.0f;

        public float mRepairHealthMax = 100.0f;
        public float mRepairSpeed = 1.0f;
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Machine
    {
        private MachineParameters _mParams;
        private Sprite _mSprite = null;

        //-------------------------------------------------------------------------
        public Machine()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, UInt32 _SceneId, MachineParameters _Params)
        {
            _mParams = _Params;

            _mSprite = SceneManager.Singleton.GetNewSprite(_mParams.mSprite.mFileName, _ContentManager, _SceneId, _Params.mSprite.mNbFrames, 0.0f);
            _mSprite.Depth = _mParams.mDepth;
            _mSprite.mPosition = _mParams.mPosition;
            _mSprite.SetBoundingBox((UInt32)CollisionId.Machine, _mParams.mBBoxOffset, _mParams.mBBoxSize);

            if (_mSprite.AnimPlayer != null)
                _mSprite.AnimPlayer.Play = false;
        }

        //------------------------------------------------------------------
        public Vector2 Position
        {
            get { return _mParams.mPosition; }
            set { _mParams.mPosition = value; _mSprite.mPosition = _mParams.mPosition; }
        }

        private float Health
        {
            get { return _mParams.mHealth; }
            set { _mParams.mHealth = MathHelper.Clamp(value, 0.0f, _mParams.mHealthMax); }
        }

        private float HealthMax
        {
            get { return _mParams.mHealthMax; }
        }

        public MachineId Id
        {
            get { return _mParams.mId; }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // Seconds
        {
            Health -= (_mParams.mDamageA + _mParams.mDamageB * Timer.Singleton.Seconds) * _Dt;

            if(_mSprite.AnimPlayer != null)
                _mSprite.AnimPlayer.CurrentFrame = (int)Math.Ceiling( (float)(_mSprite.AnimPlayer.NbFrames - 1)*(Health / HealthMax) );
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public bool Collide(UInt32 _Type)
        {
            return CollisionsManager.Singleton.Collide(_mSprite, _Type, Vector2.Zero) != null;
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Repair()
        {
            Health += _mParams.mRepairSpeed;

            switch(_mParams.mId)
            {
                case MachineId.Zeus:
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__ACTION__PRAY");
                    break;
                case MachineId.Pipes:
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__ACTION__MAINTENANCE");
                    break;
                case MachineId.EngineL:
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__ACTION__POUR");
                    break;
                case MachineId.EngineR:
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__ACTION__POUR");
                    break;
            }
        }
    }
}