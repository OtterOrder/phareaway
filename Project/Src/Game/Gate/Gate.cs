using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PhareAway
{
    public class Gate
    {
        public Vector2 _mEntrance;
        public Vector2 _mExit;

        Sprite  _mEntranceSprite;
        Sprite  _mExitSprite;

        public Gate(string _GateSprite, ContentManager _ContentManager, UInt32 _SceneId, Vector2 _Entrance, Vector2 _Exit)
        {
            _mEntrance = _Entrance;
            _mExit = _Exit;

            _mEntranceSprite = SceneManager.Singleton.GetNewSprite(_GateSprite, _ContentManager, _SceneId);
            _mEntranceSprite.Depth = 0.5f;
            _mEntranceSprite.mPosition = _Entrance;
            _mEntranceSprite.mOrigin = new Vector2((float)_mEntranceSprite.Width / 2.0f, (float)_mEntranceSprite.Height);
            _mEntranceSprite.SetBoundingBox(4, Vector2.Zero, new Vector2(_mEntranceSprite.Width, _mEntranceSprite.Height));

            _mExitSprite = SceneManager.Singleton.GetNewSprite(_GateSprite, _ContentManager, _SceneId);
            _mExitSprite.Depth = 0.5f;
            _mExitSprite.mPosition = _Exit;
            _mExitSprite.mOrigin = new Vector2((float)_mExitSprite.Width / 2.0f, (float)_mExitSprite.Height);
            _mExitSprite.SetBoundingBox(4, Vector2.Zero, new Vector2(_mExitSprite.Width, _mExitSprite.Height));
        }

        public Vector2 GetGateByEntry(Vector2 _Entry)
        {
            if ((_Entry.X > _mEntrance.X - (float)_mEntranceSprite.Width / 2.0f) && (_Entry.X < _mEntrance.X + (float)_mEntranceSprite.Width / 2.0f) &&
                (_Entry.Y > _mEntrance.Y - 10.0f) && (_Entry.Y < _mEntrance.Y + _mEntranceSprite.Height))
                return _mEntrance;
            else if ((_Entry.X > _mExit.X - (float)_mExitSprite.Width / 2.0f) && (_Entry.X < _mExit.X + (float)_mExitSprite.Width / 2.0f) &&
                (_Entry.Y > _mExit.Y - 10.0f) && (_Entry.Y < _mExit.Y + _mExitSprite.Height))
                return _mExit;
            else
                return Vector2.Zero;
        }

        public Vector2 GetExitByEntry(Vector2 _Entry)
        {
            if (_Entry == _mEntrance)
                return _mExit;
            else if (_Entry == _mExit)
                return _mEntrance;
            else
                return _Entry;
            
        }
    }
}
