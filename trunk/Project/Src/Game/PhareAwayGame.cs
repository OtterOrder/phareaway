﻿using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace PhareAway
{
    public enum LevelName
    {
        Level_Main,
        Level_Menu,
        Level_Logos,
        Level_Tuto,
    }

    public class PhareAwayGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _mGraphics;
        private SpriteBatch           _mSpriteBatch;
        private ContentManager        _mContent;

        private LevelName             _mStartLevel;

        private Level                 _mCurrentLevel;
        private Level                 _mMainLevel;
        private Level                 _mMenuLevel;
        private Level                 _mLogosLevel;
        private Level                 _mTutoLevel;

        public const int mBackBufferWidth = 1280;
        public const int mBackBufferHeight = 720;

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

            _mMainLevel = new LevelMain(this, _mContent);
            _mMainLevel.Init();
            _mMenuLevel = new LevelMenu(this, _mContent);
            _mMenuLevel.Init();
            _mLogosLevel = new LevelLogos(this, _mContent);
            _mLogosLevel.Init();
            _mTutoLevel = new LevelTuto(this, _mContent);
            _mTutoLevel.Init();

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
                    _mCurrentLevel = _mMainLevel; break;
                case LevelName.Level_Menu:
                    _mCurrentLevel = _mMenuLevel; break;
                case LevelName.Level_Logos:
                    _mCurrentLevel = _mLogosLevel; break;
                case LevelName.Level_Tuto:
                    _mCurrentLevel = _mTutoLevel; break;
            }
        }
    }
}