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
    public class GateManager
    {

        private List<Gate> _mGateList = new List<Gate>();

        public GateManager()
        {

        }

        public void AddGate(string _GateSprite, ContentManager _ContentManager, UInt32 _SceneId, Vector2 _Entrance, Vector2 _Exit)
        {
            Gate gate = new Gate(_GateSprite, _ContentManager, _SceneId, _Entrance, _Exit);

        }

        public Gate GetGateByPosition()
        {
            return null;
        }

    }
}
