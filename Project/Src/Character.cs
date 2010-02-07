using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class SpriteParameters
    {
        public string mFileName = "";
        public UInt32 mNbFrames = 1;
        public float  mFps = 0.0f;
        public bool   mLoop = false;
    }

    public class CharacterParameters
    {
        public string    mFileBase = "";
        public SpriteParameters[] mSpritesParams = null;

        public CharacterParameters ()
        {
            mSpritesParams = new SpriteParameters[Character.NbSprites];

            for (int i = 0; i < Character.NbSprites; i++)
                mSpritesParams[i] = new SpriteParameters();
        }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Character
    {
        public const int NbSprites = 4;

        public enum State
        {
            Idle = 0,
            Walk = 1,
            Jump = 2,
            Fall = 3
        }

        private State       _mState = (int)State.Idle;

        private Sprite[]    _mSprites = null;

        private float       _mGravity = 0.001f;
        private Vector2      _mPosition = new Vector2();
        // Temp ?
        public Vector2 GetPosition() { return _mPosition; }

        private Vector2     _mSpeed = new Vector2();

        //-------------------------------------------------------------------------
        public Character()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, CharacterParameters _Parameters, UInt32 _SceneId)
        {
            _mSprites = new Sprite[NbSprites];

            for (int i = 0; i < NbSprites; i++)
            {
                _mSprites[i] = SceneManager.Singleton.GetNewSprite(_Parameters.mFileBase + _Parameters.mSpritesParams[i].mFileName, _ContentManager, _Parameters.mSpritesParams[i].mNbFrames, _Parameters.mSpritesParams[i].mFps, _SceneId);
                if (_mSprites[i].AnimPlayer != null)
                    _mSprites[i].AnimPlayer.Loop = _Parameters.mSpritesParams[i].mLoop;

                _mSprites[i].SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSprites[i].Width, _mSprites[i].Height));
                _mSprites[i].mVisible = false;
            }

            _mSprites[(int)_mState].mVisible = true;
        }

        //------------------------------------------------------------------
        private Sprite GetCurrentSprite()
        {
            return _mSprites[(int)_mState];
        }

        private void SetCurrentSprite(State _State)
        {
            int Idx = (int)_State;

            GetCurrentSprite().mVisible = false;
            _mSprites[Idx].mVisible = true;
            _mSprites[Idx].mPosition = GetCurrentSprite().mPosition;
            _mSprites[Idx].mScale = GetCurrentSprite().mScale;
            _mSprites[Idx].mFlip = GetCurrentSprite().mFlip;

            if (_mSprites[Idx].AnimPlayer != null)
            {
                _mSprites[Idx].AnimPlayer.CurrentFrame = 0;
                _mSprites[Idx].AnimPlayer.Play = true;
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            UpdateInputs(_Dt);

            UpdatePhysique(_Dt);

            if (_mSpeed.Y < 0.0f)
                ChangeState(State.Jump);
            else
            if (_mSpeed.Y >= 0.0f && _mGravity != 0.0f)
                ChangeState(State.Fall);
            else
            if (_mSpeed.X != 0.0f)
                ChangeState(State.Walk);
            else
                ChangeState(State.Idle);

            if (_mSpeed.X > 0.0f)
                GetCurrentSprite().mFlip = SpriteEffects.FlipHorizontally;
            else
            if (_mSpeed.X < 0.0f)
                GetCurrentSprite().mFlip = SpriteEffects.None;

            _mSprites[(int)_mState].mPosition = _mPosition;
        }

        //-----------------------------------
        private void UpdatePhysique(float _Dt)
        {
            // If the character is in air, apply the gravity
            if (CollisionsManager.Singleton.Collide(GetCurrentSprite(), 2, new Vector2(0.0f, 0.5f)) == null)
            {
                _mGravity = 0.01f;
            }

            _mSpeed.Y += _mGravity * _Dt;

            // Update vertical collisions
            if (_mSpeed.Y != 0.0f)
            {
                BoundingBox Collision = CollisionsManager.Singleton.Collide(GetCurrentSprite(), 2, new Vector2(0.0f, _mSpeed.Y));
                if (Collision != null)
                {
                    if (_mSpeed.Y >= 0.0f)
                        _mPosition.Y = Collision.Top - GetCurrentSprite().GetBoundingBox().Size.Y - 0.1f;
                    else
                        _mPosition.Y = Collision.Bottom + 0.1f;

                    _mSpeed.Y = 0.0f;
                    _mGravity = 0.0f;
                }
                else
                {
                    _mPosition.Y += _mSpeed.Y;
                }
            }

            // Update horizontal collisions
            if (_mSpeed.X != 0.0f)
            {
                BoundingBox Collision = CollisionsManager.Singleton.Collide(GetCurrentSprite(), 2, new Vector2(_mSpeed.X, 0.0f));
                if (Collision != null)
                {
                    if (_mSpeed.X >= 0.0f)
                        _mPosition.X = Collision.Left - GetCurrentSprite().GetBoundingBox().Size.X - 0.1f;
                    else
                        _mPosition.X = Collision.Right + 0.1f;

                    _mSpeed.X = 0.0f;
                }
                else
                {
                    _mPosition.X += _mSpeed.X;
                }
            }
        }

        //-----------------------------------
        private void UpdateInputs(float _Dt)
        {
            KeyboardState lKeyboardState = Keyboard.GetState();

            float Speed = 0.1f * _Dt;

            _mSpeed.X = 0.0f;

            if (lKeyboardState.IsKeyDown(Keys.Right))
                _mSpeed.X = Speed;

            if (lKeyboardState.IsKeyDown(Keys.Left))
                _mSpeed.X = -Speed;

            if (lKeyboardState.IsKeyDown(Keys.Up))
                _mSpeed.Y = -Speed;
        }

        //-----------------------------------
        private void ChangeState (State _State)
        {
            if (_State == _mState)
                return;

            switch (_State)
            {
                case State.Idle:
                    SetCurrentSprite(State.Idle);
                    break;

                case State.Walk:
                    SetCurrentSprite(State.Walk);
                    break;

                case State.Jump:
                    SetCurrentSprite(State.Jump);
                    break;

                case State.Fall:
                    SetCurrentSprite(State.Fall);
                    break;
            }

            _mState = _State;
        }
    }
}