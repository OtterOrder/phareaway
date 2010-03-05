using System;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    public class InputManager
    {
        private static InputManager _mSingleton = null;

        private KeyboardState _mCurrentKeyBoardState;
        private KeyboardState _mLastKetBoardState;

        //-------------------------------------------------------------------------
        private InputManager()
        {
            _mLastKetBoardState = _mCurrentKeyBoardState = Keyboard.GetState();
        }

        //-------------------------------------------------------------------------
        public static InputManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new InputManager();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        public void Update ()
        {
            _mLastKetBoardState = _mCurrentKeyBoardState;
            _mCurrentKeyBoardState = Keyboard.GetState();
        }

        //-------------------------------------------------------------------------
        public bool IsKeyPressed (Keys _Key)
        {
            return _mCurrentKeyBoardState.IsKeyDown(_Key);
        }

        public bool IsKeyReleased(Keys _Key)
        {
            return _mCurrentKeyBoardState.IsKeyUp(_Key);
        }

        public bool IsKeyJustPressed (Keys _Key)
        {
            return (_mCurrentKeyBoardState.IsKeyDown(_Key) && _mLastKetBoardState.IsKeyUp(_Key));
        }

        public bool IsKeyJustReleased(Keys _Key)
        {
            return (_mCurrentKeyBoardState.IsKeyUp(_Key) && _mLastKetBoardState.IsKeyDown(_Key));
        }
    }
}