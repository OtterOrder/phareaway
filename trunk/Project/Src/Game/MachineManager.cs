using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

namespace PhareAway
{
    //-------------------------------------------------------------------------
    //-------------------------------------------------------------------------
    public class MachineManager
    {
        private static MachineManager _mSingleton = null;

        private List<Machine> _mMachineList = new List<Machine>();

        //-------------------------------------------------------------------------
        private MachineManager()
        {
        }

        //-------------------------------------------------------------------------
        public static MachineManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new MachineManager();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void AddMachine(ContentManager _ContentManager, UInt32 _SceneId, MachineParameters _Params)
        {
            Machine machine = new Machine();
            machine.Init(_ContentManager, _SceneId, _Params);
            _mMachineList.Add(machine);
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            float Dt = _Dt / 1000.0f;

            foreach (Machine It in _mMachineList)
            {
                It.Update(Dt);
            }
        }

        //-------------------------------------------------------------------------
        //-------------------------------------------------------------------------
        public Machine Collide (UInt32 _Type)
        {
            foreach (Machine It in _mMachineList)
            {
                if (It.Collide(_Type))
                    return It;
            }

            return null;
        }
    }
}