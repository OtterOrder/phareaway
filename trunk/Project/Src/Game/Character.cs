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

    public class CGameParameters
    {
        public float mWalkSpeed  = 0.15f;
        public float mJumpSpeed  = 0.2f;
        public float mFallSpeed  = 0.4f;
        public float mClimbSpeed = 0.1f;
    }

    public class InputParameters
    {
        public Keys mRight  = 0;
        public Keys mLeft   = 0;
        public Keys mUp     = 0;
        public Keys mDown   = 0;
        public Keys mJump   = 0;
        public Keys mAction = 0;
    }

    public class CharacterParameters
    {
        public string    mFileBase = "";
        public SpriteParameters[] mSpritesParams = null;
        public SpriteParameters[] mMachinesSpritesParams = null;
        public CGameParameters mGameParams = new CGameParameters();
        public InputParameters mInputParams = new InputParameters();
        public float mDepth = 0.5f;
        public UInt32 mCollisionId;

        public CharacterParameters ()
        {
            mSpritesParams = new SpriteParameters[Character.NbSprites];
            mMachinesSpritesParams = new SpriteParameters[PhareAwayGame.NbMachines];

            for (int i = 0; i < Character.NbSprites; i++)
                mSpritesParams[i] = new SpriteParameters();

            for (int i = 0; i < PhareAwayGame.NbMachines; i++)
                mMachinesSpritesParams[i] = new SpriteParameters();
        }
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Character
    {
        public const int NbSprites = 5;

        private CGameParameters _mGameParams = null;
        private InputParameters _mInputParams = null;

        public enum State
        {
            Idle        = 0,
            Walk        = 1,
            Jump        = 2,
            Fall        = 3,
            Climb       = 4,
            Machine     = 5,
            MachineWait = 6
        }

        public bool             mActive = true;

        private State           _mState = State.Idle;
        private MachineId       _mMachineState = MachineId.None;

        private Sprite[]        _mSprites = null;
        private Sprite[]        _mMachinesSprites = null;

        public  float           mGravityValue = 0.01f;
        private float           _mGravity = 0.0f;
        private Vector2         _mPosition = new Vector2();

        private Vector2         _mSpeed = new Vector2();

        private UInt32          _mCollisionId;

        //-------------------------------------------------------------------------
        public Character()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager, CharacterParameters _Parameters, UInt32 _SceneId)
        {
            _mCollisionId = _Parameters.mCollisionId;

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

                _mSprites[i].Depth = _Parameters.mDepth;
                _mSprites[i].mOrigin = new Vector2((float)_mSprites[i].Width / 2.0f, (float)_mSprites[i].Height);
                _mSprites[i].SetBoundingBox(_mCollisionId, new Vector2(0.0f, 0.0f), new Vector2(_mSprites[i].Width, _mSprites[i].Height));
                _mSprites[i].mVisible = false;
                _mSprites[i].GetBoundingBox().Active = false;
            }

            _mSprites[(int)_mState].mVisible = true;
            _mSprites[(int)_mState].GetBoundingBox().Active = false;


            _mMachinesSprites = new Sprite[PhareAwayGame.NbMachines];

            for (int i = 0; i < PhareAwayGame.NbMachines; i++)
            {
                _mMachinesSprites[i] = SceneManager.Singleton.GetNewSprite( _Parameters.mFileBase + _Parameters.mMachinesSpritesParams[i].mFileName,
                                                                            _ContentManager,
                                                                            _SceneId,
                                                                            _Parameters.mMachinesSpritesParams[i].mNbFrames,
                                                                            _Parameters.mMachinesSpritesParams[i].mFps);
                if (_mMachinesSprites[i].AnimPlayer != null)
                    _mMachinesSprites[i].AnimPlayer.Loop = _Parameters.mMachinesSpritesParams[i].mLoop;

                _mMachinesSprites[i].Depth = _Parameters.mDepth;
                _mMachinesSprites[i].mOrigin = new Vector2((float)_mMachinesSprites[i].Width / 2.0f, (float)_mMachinesSprites[i].Height);
                _mMachinesSprites[i].SetBoundingBox(_mCollisionId, new Vector2(0.0f, 0.0f), new Vector2(_mMachinesSprites[i].Width, _mMachinesSprites[i].Height));
                _mMachinesSprites[i].mVisible = false;
                _mMachinesSprites[i].GetBoundingBox().Active = false;
            }

            _mGameParams = _Parameters.mGameParams;
            _mInputParams = _Parameters.mInputParams;
        }

        //------------------------------------------------------------------
        public Vector2 GetPosition() { return _mPosition; }

        public void SetPosition(Vector2 _Position) { _mPosition = _Position; }

        private Sprite GetCurrentSprite()
        {
            if (_mState != State.Machine && _mState != State.MachineWait)
                return _mSprites[(int)_mState];
            else
                return _mMachinesSprites[(int)_mMachineState];
        }

        private Sprite GetSprite(State _State)
        {
            return _mSprites[(int)_State];
        }

        private void SetCurrentSprite(State _State)
        {
            int Idx = (int)_State;

            GetCurrentSprite().mVisible = false;
            GetCurrentSprite().GetBoundingBox().Active = false;
            _mSprites[Idx].mVisible = true;
            _mSprites[Idx].mPosition = GetCurrentSprite().mPosition;
            _mSprites[Idx].mScale = GetCurrentSprite().mScale;
            _mSprites[Idx].mFlip = GetCurrentSprite().mFlip;
            _mSprites[Idx].GetBoundingBox().Active = true;

            if (_mSprites[Idx].AnimPlayer != null)
            {
                _mSprites[Idx].AnimPlayer.CurrentFrame = 0;
                _mSprites[Idx].AnimPlayer.Play = true;
            }
        }

        private void SetCurrentSprite(MachineId _State)
        {
            int Idx = (int)_State;

            GetCurrentSprite().mVisible = false;
            GetCurrentSprite().GetBoundingBox().Active = false;
            _mMachinesSprites[Idx].mVisible = true;
            _mMachinesSprites[Idx].mPosition = GetCurrentSprite().mPosition;
            _mMachinesSprites[Idx].mScale = GetCurrentSprite().mScale;
            _mMachinesSprites[Idx].GetBoundingBox().Active = true;

            if (_mMachinesSprites[Idx].AnimPlayer != null)
            {
                _mMachinesSprites[Idx].AnimPlayer.Restart();
                _mMachinesSprites[Idx].AnimPlayer.Play = true;
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

        public InputParameters InputParameters
        {
            get { return _mInputParams; }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            if (mActive)
            {
                UpdateInputs(_Dt);
                UpdateLadder(_Dt);
                UpdateGate(_Dt);
                UpdateMachine();
            }

            if (_mState != State.Climb &&
                _mState != State.Machine)
            {
                UpdatePhysique(_Dt);
                UpdateDisplacement();
            }

            if (_mState == State.Machine && GetCurrentSprite().AnimPlayer != null)
            {
                _mSpeed = Vector2.Zero;
                if (GetCurrentSprite().AnimPlayer.AtEnd)
                    ChangeState(State.MachineWait);
            }

            _mPosition += _mSpeed;
            GetCurrentSprite().mPosition = _mPosition;
        }

        //-----------------------------------
        private void ChangeState(State _State)
        {
            if (_State == _mState || (_mState == State.Machine && _State != State.MachineWait))
                return;

            switch (_State)
            {
                case State.Idle:
                    if (_mState == State.MachineWait)
                        return;
                    else
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

                case State.Climb:
                    SetCurrentSprite(State.Climb);
                    break;
            }

            if (_State != State.MachineWait)
                _mMachineState = MachineId.None;

            _mState = _State;
        }

        //-----------------------------------
        private void ChangeState(MachineId _State)
        {
            if (_State == _mMachineState && _mState == State.Machine)
                return;

            /*switch (_State)
            {
                case MachineId.Pipes:
                    SetCurrentSprite(MachineId.Pipes);
                    break;

                case MachineId.Zeus:
                    SetCurrentSprite(MachineId.Zeus);
                    break;
            }*/

            SetCurrentSprite(_State);

            ChangeState(State.Machine);

            _mMachineState = _State;
        }

        //-----------------------------------
        private void UpdateInputs(float _Dt)
        {
            _mSpeed.X = 0.0f;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mRight))
                _mSpeed.X = _mGameParams.mWalkSpeed * _Dt;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mLeft))
                _mSpeed.X = -_mGameParams.mWalkSpeed * _Dt;

            if (InputManager.Singleton.IsKeyJustPressed(_mInputParams.mJump) && (_mState == State.Idle || _mState == State.Walk || _mState == State.MachineWait))
                _mSpeed.Y = -_mGameParams.mJumpSpeed * _Dt;
        }

        //-----------------------------------
        private void UpdatePhysique(float _Dt)
        {
            // If the character is in air, apply the gravity
            if (CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Ground, new Vector2(0.0f, 0.5f)) == null)
            {
                _mGravity = mGravityValue;
            }

            _mSpeed.Y = Math.Min(_mSpeed.Y + _mGravity * _Dt, _mGameParams.mFallSpeed*_Dt);

            // Update vertical collisions
            if (_mSpeed.Y != 0.0f)
            {
                BoundingBox Collision = CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Ground, new Vector2(0.0f, _mSpeed.Y));
                if (Collision != null)
                {
                    if (_mSpeed.Y >= 0.0f)
                        PlaceToTop(Collision);
                    else
                        PlaceToBottom(Collision);

                    _mSpeed.Y = 0.0f;
                    _mGravity = 0.0f;
                }
            }

            // Update horizontal collisions
            if (_mSpeed.X != 0.0f)
            {
                BoundingBox Collision = CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Ground, new Vector2(_mSpeed.X, 0.0f));
                if (Collision != null)
                {
                    if (_mSpeed.X >= 0.0f)
                        PlaceToLeft(Collision);
                    else
                        PlaceToRight(Collision);

                    _mSpeed.X = 0.0f;
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
        private void UpdateDisplacement()
        {
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
        }

        //-----------------------------------
        private void UpdateLadder(float _Dt)
        {
            BoundingBox Ground = CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Ground, Vector2.Zero);
            if (_mSpeed.X != 0.0f && Ground == null)
            {
                ChangeState(State.Idle);
                return;
            }

            BoundingBox Ladder  = CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Ladder, Vector2.Zero);
            if (Ladder == null)
                return;

            float Direction = 0.0f;

            bool Up      = InputManager.Singleton.IsKeyPressed(_mInputParams.mUp);
            bool Down    = InputManager.Singleton.IsKeyPressed(_mInputParams.mDown);

            if( Up ^ Down)
            {
                if( Up )
                    Direction = -1.0f;
                else
                    Direction = 1.0f;

                ChangeState(State.Climb);

                if( Up && Ground == null )
                {
                    if( Ladder.Top >= (BBox.Top - _mGameParams.mClimbSpeed -1.0f) &&
                        CollisionsManager.Singleton.Collide(new Vector2(Ladder.Right, Ladder.Top - 1.0f), (UInt32)CollisionId.Ladder) == null)
                    {
                        ChangeState(State.Idle);
                    }
                }

                if( Down )
                {
                    if (Ladder.Bottom <= (BBox.Bottom - _mGameParams.mClimbSpeed + 1.0f) &&
                        CollisionsManager.Singleton.Collide(new Vector2(Ladder.Right, Ladder.Bottom + 1.0f), (UInt32)CollisionId.Ladder) == null)
                    {
                        ChangeState(State.Idle);
                    }
                }
            }

            if( _mState == State.Climb)
            {
                _mPosition.X = Ladder.Sprite.mPosition.X;

                _mSpeed.X = 0.0f;
                _mSpeed.Y = Direction * _mGameParams.mClimbSpeed * _Dt;

                GetCurrentSprite().mFlip = SpriteEffects.None;
                if (GetCurrentSprite().AnimPlayer != null)
                    GetCurrentSprite().AnimPlayer.Speed = Direction;
            }
        }

        //-----------------------------------
        private void UpdateGate(float _Dt)
        {
            BoundingBox Gate = CollisionsManager.Singleton.Collide(GetCurrentSprite(), (UInt32)CollisionId.Gate, Vector2.Zero);
            if (Gate == null)
                return;

            bool Up = InputManager.Singleton.IsKeyJustPressed(_mInputParams.mUp);

            if (Up)
            {
                Vector2 newPos = GateManager.Singleton.GetExitGateByPosition(_mPosition);
                if (newPos != _mPosition)
                {
                    _mPosition = newPos;
                    _mPosition.Y -= 2;
                }
            }
        }

        //-----------------------------------
        private void UpdateMachine()
        {
            if (_mState != State.Idle &&
                _mState != State.Walk &&
                _mState != State.MachineWait)
                return;

            if (InputManager.Singleton.IsKeyPressed(_mInputParams.mAction))
            {
                Machine machine = MachineManager.Singleton.Collide(_mCollisionId);
                if (machine != null && _mState != State.Machine)
                {
                    ChangeState(machine.Id);
                    machine.Repair();
                }
            }
        }
    }
}