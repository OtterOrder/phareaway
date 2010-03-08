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
        private static GateManager _mSingleton = null;

        private List<Gate> _mGateList = new List<Gate>();

        public static GateManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new GateManager();

                return _mSingleton;
            }
        }

        public GateManager()
        {

        }

        //-------------------------------------------------------------------------
        public void AddGate(string _GateSprite, ContentManager _ContentManager, UInt32 _SceneId, Vector2 _Entrance, Vector2 _Exit)
        {
            Gate gate = new Gate(_GateSprite, _ContentManager, _SceneId, _Entrance, _Exit);
            _mGateList.Add(gate);

        }

        public Vector2 GetExitGateByPosition(Vector2 _GatePosition)
        {
            foreach (Gate gate in _mGateList)
            {
                Vector2 pos = gate.GetGateByEntry(_GatePosition);
                if (pos != Vector2.Zero)
                {
                    return gate.GetExitByEntry(pos);
                }
            }
            return _GatePosition;
        }

    }
}
