using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace PhareAway
{
    public class SoundManager
    {
        private static SoundManager _mSingleton = null;

        private AudioEngine _mAudioEngine;
        public  SoundBank _mSoundSoundBank;
        public  SoundBank _mMusicSoundBank;
        private WaveBank _mSoundWaveBank;
        private WaveBank _mMusicWaveBank;

        private SoundManager()
        {
        }

        public static SoundManager Singleton
        {
            get
            {
                if (_mSingleton == null)
                    _mSingleton = new SoundManager();

                return _mSingleton;
            }
        }

        public void Init()
        {
            _mAudioEngine = new AudioEngine("Resources/Sounds/PhareAway.xgs");
            _mSoundSoundBank = new SoundBank(_mAudioEngine, "Resources/Sounds/Sounds.xsb");
            _mMusicSoundBank = new SoundBank(_mAudioEngine, "Resources/Sounds/Musics.xsb");
            _mSoundWaveBank = new WaveBank(_mAudioEngine, "Resources/Sounds/Sounds.xwb");
            _mMusicWaveBank = new WaveBank(_mAudioEngine, "Resources/Sounds/Musics.xwb", 0, 250);
        }

        public void Update()
        {
            _mAudioEngine.Update();
        }
    }
}
