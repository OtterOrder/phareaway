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

        public  float       mGravityValue = 0.01f;
        private float       _mGravity = 0.0f;
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
                _mSprites[i] = SceneManager.Singleton.GetNewSprite( _Parameters.mFileBase + _Parameters.mSpritesParams[i].mFileName,
                                                                    _ContentManager,
                                                                    _SceneId,
                                                                    _Parameters.mSpritesParams[i].mNbFrames,
                                                                    _Parameters.mSpritesParams[i].mFps);
                if (_mSprites[i].AnimPlayer != null)
                    _mSprites[i].AnimPlayer.Loop = _Parameters.mSpritesParams[i].mLoop;

                _mSprites[i].mOrigin = new Vector2((float)_mSprites[i].Width / 2.0f, (float)_mSprites[i].Height);

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

        private Sprite GetSprite(State _State)
        {
            return _mSprites[(int)_State];
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

        private BoundingBox BBox
        {
            get
            {
                GetCurrentSprite().GetBoundingBox().Update();
                return GetCurrentSprite().GetBoundingBox();
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
                _mGravity = mGravityValue;
            }

            _mSpeed.Y += _mGravity * _Dt;

            // Update vertical collisions
            if (_mSpeed.Y != 0.0f)
            {
                BoundingBox Collision = CollisionsManager.Singleton.Collide(GetCurrentSprite(), 2, new Vector2(0.0f, _mSpeed.Y));
                if (Collision != null)
                {
                    if (_mSpeed.Y >= 0.0f)
                        PlaceToTop(Collision); //_mPosition.Y = Collision.Top - (BBox.mPostion.Y + BBox.Size.Y - GetCurrentSprite().mOrigin.Y) - 0.1f;
                    else
                        PlaceToBottom(Collision); //_mPosition.Y = Collision.Bottom + (GetCurrentSprite().mOrigin.Y - BBox.mPostion.Y) + 0.1f;

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
                        PlaceToLeft(Collision);
                    else
                        PlaceToRight(Collision);

                    _mSpeed.X = 0.0f;
                }
                else
                {
                    _mPosition.X += _mSpeed.X;
                }
            }
        }

        private void PlaceToTop(BoundingBox _BBox)
        {
            _mPosition.Y = _BBox.Top - (BBox.mPostion.Y + BBox.Size.Y - GetCurrentSprite().mOrigin.Y) - 0.1f;
        }

        private void PlaceToBottom(BoundingBox _BBox)
        {
            _mPosition.Y = _BBox.Bottom + (GetCurrentSprite().mOrigin.Y - BBox.mPostion.Y) + 0.1f;
        }

        private void PlaceToLeft (BoundingBox _BBox)
        {
            _mPosition.X = _BBox.Left - (BBox.mPostion.X + BBox.Size.X - GetCurrentSprite().mOrigin.X) - 0.1f;
        }

        private void PlaceToRight(BoundingBox _BBox)
        {
            _mPosition.X = _BBox.Right + (GetCurrentSprite().mOrigin.X - BBox.mPostion.X) + 0.1f;
        }

        //-----------------------------------
        private void UpdateInputs(float _Dt)
        {
            KeyboardState lKeyboardState = Keyboard.GetState();

            float Speed = 0.1f * _Dt;

            _mSpeed.X = 0.0f;

            if (InputManager.Singleton.IsKeyPressed(Keys.Right))
                _mSpeed.X = Speed;

            if (InputManager.Singleton.IsKeyPressed(Keys.Left))
                _mSpeed.X = -Speed;

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Up) && (_mState == State.Idle || _mState == State.Walk))
                _mSpeed.Y = -Speed*4.0f;
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