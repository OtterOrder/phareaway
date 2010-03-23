using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;

namespace PhareAway
{
    public enum LevelName
    {
        Level_Logos,
        Level_Menu,
        Level_Tuto,
        Level_Main,    
        Level_Credits,
        Level_Intro,
    }

    public enum CollisionId
    {
        Archi,
        Philo,
        Ground,
        Ladder,
        Lighthouse,
        Obstacle,
        Gate,
        Machine
    }

    public enum MachineId
    {
        None = -1,
        Pipes = 0,
        Zeus = 1,
        EngineL = 2,
        EngineR = 3
    }

    public class PhareAwayGame : Microsoft.Xna.Framework.Game
    {
        public static int NbMachines = 4;

        private GraphicsDeviceManager _mGraphics;
        private SpriteBatch           _mSpriteBatch;
        private ContentManager        _mContent;

        private LevelName             _mStartLevel;

        private Level                 _mCurrentLevel;
        private Level                 _mMainLevel;
        private Level                 _mMenuLevel;
        private Level                 _mLogosLevel;
        private Level                 _mTutoLevel;
        private Level                 _mCreditsLevel;
        private Level                 _mIntroLevel;

        public const int mBackBufferWidth = 1280;
        public const int mBackBufferHeight = 720;

        private AudioEngine _mAudioEngine;
        private SoundBank   _mSoundSoundBank;
        private SoundBank   _mMusicSoundBank;
        private WaveBank    _mSoundWaveBank;
        private WaveBank    _mMusicWaveBank;

        public PhareAwayGame(LevelName _StartLevel)
        {
            _mGraphics = new GraphicsDeviceManager(this);
            _mGraphics.PreferredBackBufferWidth = mBackBufferWidth;
            _mGraphics.PreferredBackBufferHeight = mBackBufferHeight;

            _mStartLevel = _StartLevel;

            Content.RootDirectory = "Resources";

        }

        protected override void LoadContent()
        {
            _mSpriteBatch = new SpriteBatch(GraphicsDevice);
            _mContent = new ContentManager(Services, "Resources");

            _mAudioEngine = new AudioEngine("Resources/Sounds/PhareAway.xgs");
            _mSoundSoundBank = new SoundBank(_mAudioEngine, "Resources/Sounds/Sounds.xsb");
            _mMusicSoundBank = new SoundBank(_mAudioEngine, "Resources/Sounds/Musics.xsb");
            _mSoundWaveBank = new WaveBank(_mAudioEngine, "Resources/Sounds/Sounds.xwb");
            _mMusicWaveBank = new WaveBank(_mAudioEngine, "Resources/Sounds/Musics.xwb", 0, 4);

            _mMainLevel = new LevelMain(this, _mContent);
            _mMainLevel.Init();
            _mMenuLevel = new LevelMenu(this, _mContent);
            _mMenuLevel.Init();
            _mLogosLevel = new LevelLogos(this, _mContent);
            _mLogosLevel.Init();
            _mTutoLevel = new LevelTuto(this, _mContent);
            _mTutoLevel.Init();
            _mCreditsLevel = new LevelCredits(this, _mContent);
            _mCreditsLevel.Init();
            _mIntroLevel = new LevelIntro(this, _mContent);
            _mIntroLevel.Init();

            switch (_mStartLevel)
            {
                case LevelName.Level_Main:
                    _mCurrentLevel = _mMainLevel; break;
                case LevelName.Level_Menu:
                    _mCurrentLevel = _mMenuLevel; break;
                case LevelName.Level_Logos:
                    _mCurrentLevel = _mLogosLevel; break;
                case LevelName.Level_Tuto:
                    _mCurrentLevel = _mTutoLevel; break;
                case LevelName.Level_Credits:
                    _mCurrentLevel = _mCreditsLevel; break;
                case LevelName.Level_Intro:
                    _mCurrentLevel = _mIntroLevel; break;
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.Singleton.IsKeyPressed(Keys.Escape))
                Exit();

            base.Update(gameTime);

            Timer.Singleton.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            InputManager.Singleton.Update();

            float Dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            _mCurrentLevel.Update(Dt);
            SceneManager.Singleton.Update(Dt);

            _mAudioEngine.Update();
        }

        protected override void Draw(GameTime gameTime)
        {
            _mSpriteBatch.GraphicsDevice.Clear(Color.DeepPink);

            _mCurrentLevel.Draw(_mSpriteBatch, _mGraphics);

            base.Draw(gameTime);
        }

        public void ChangeLevel(LevelName _Level)
        {
            switch (_Level)
            {
                case LevelName.Level_Main:
                    _mCurrentLevel = _mMainLevel;break;
                case LevelName.Level_Menu:
                    _mCurrentLevel = _mMenuLevel;
                    _mMusicSoundBank.PlayCue("MUSIC__MENU"); break;
                case LevelName.Level_Logos:
                    _mCurrentLevel = _mLogosLevel; break;
                case LevelName.Level_Tuto:
                    _mCurrentLevel = _mTutoLevel; break;
                case LevelName.Level_Credits:
                    _mCurrentLevel = _mCreditsLevel; break;
                case LevelName.Level_Intro:
                    _mCurrentLevel = _mIntroLevel; break;
            }
        }
    }
}
