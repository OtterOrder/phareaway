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
        Vector2 _mEntrance;
        Vector2 _mExit;

        Sprite  _mGateSprite;

        public Gate(string _GateSprite, ContentManager _ContentManager, UInt32 _SceneId, Vector2 _Entrance, Vector2 _Exit)
        {
            _mGateSprite = SceneManager.Singleton.GetNewSprite(_GateSprite, _ContentManager, _SceneId);

            _mEntrance = _Entrance;
            _mExit = _Exit;
        }
    }
}
