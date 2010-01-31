using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class ArchiParameters
    {
        public static int       mNbSprites  = 4;
        public static string    mFileBase   = "Graphics/Sprites/Inside/Characters/Archi/";
        public static string    mIdleFile   = "Archi_Idle";
        public static string    mWalkFile   = "Archi_Walk";
        public static string    mJumpFile   = "Archi_Jump";
        public static string    mFallFile   = "Archi_Fall";
    }

    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class Character
    {
        public enum State
        {
            Idle = 0,
            Walk = 1,
            Jump = 2,
            Fall = 3
        }

        private int         _mState = (int)State.Idle;

        private Sprite[]    _mSprites = null;

        private float       _mGravity = 0.001f;
        private Vector2     _mPosition = new Vector2();
        private Vector2     _mSpeed = new Vector2();

        //-------------------------------------------------------------------------
        public Character()
        {
        }

        //-------------------------------------------------------------------------
        public void Init(ContentManager _ContentManager)
        {
            _mSprites = new Sprite[ArchiParameters.mNbSprites];

            _mSprites[(int)State.Idle]  = SceneManager.Singleton.GetNewSprite(ArchiParameters.mFileBase + ArchiParameters.mIdleFile, _ContentManager);
            _mSprites[(int)State.Walk]  = SceneManager.Singleton.GetNewSprite(ArchiParameters.mFileBase + ArchiParameters.mWalkFile, _ContentManager, 4, 15);
            _mSprites[(int)State.Jump]  = SceneManager.Singleton.GetNewSprite(ArchiParameters.mFileBase + ArchiParameters.mJumpFile, _ContentManager);
            _mSprites[(int)State.Fall]  = SceneManager.Singleton.GetNewSprite(ArchiParameters.mFileBase + ArchiParameters.mFallFile, _ContentManager);

            for (int i = 0; i < ArchiParameters.mNbSprites; i++)
            {
                _mSprites[i].SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSprites[i].Width, _mSprites[i].Height));
                _mSprites[i].mVisible = false;
            }

            _mSprites[_mState].mVisible = true;
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)  // MilliSeconds
        {
            UpdateInputs(_Dt);

            if (CollisionsManager.Singleton.Collide(_mSprites[_mState], 2, new Vector2(0.0f, 1.0f)) == null)
            {
                _mGravity = 0.01f;
            }

            _mSpeed.Y += _mGravity * _Dt;

            BoundingBox Collision = CollisionsManager.Singleton.Collide(_mSprites[_mState], 2, new Vector2(0.0f, _mSpeed.Y));
            if (Collision != null)
            {
                if (_mSpeed.Y >= 0.0f)
                    _mPosition.Y = Collision.Top - _mSprites[_mState].GetBoundingBox().Size.Y - 0.5f;
                else
                    _mPosition.Y = Collision.Bottom;

                _mSpeed.Y = 0.0f;
                _mGravity = 0.0f;
            }
            else
            {
                _mPosition.Y += _mSpeed.Y;
            }

            _mPosition.X += _mSpeed.X;

            _mSprites[_mState].mPosition = _mPosition;
        }

        //-------------------------------------------------------------------------
        private void UpdateInputs(float _Dt)
        {
            KeyboardState lKeyboardState = Keyboard.GetState();

            float Speed = 0.2f * _Dt;

            _mSpeed.X = 0.0f;

            if (lKeyboardState.IsKeyDown(Keys.Right))
                _mSpeed.X = Speed;

            if (lKeyboardState.IsKeyDown(Keys.Left))
                _mSpeed.X = -Speed;

            if (lKeyboardState.IsKeyDown(Keys.Up))
                _mSpeed.Y = -Speed;
        }
    }
}