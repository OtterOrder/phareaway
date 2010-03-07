using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace PhareAway
{
    public class PhareAwayGame : Microsoft.Xna.Framework.Game
    {
        private GraphicsDeviceManager _mGraphics;
        private SpriteBatch           _mSpriteBatch;
        private ContentManager        _mContent;

        private Level                 _mLevelMain;

        public const int mBackBufferWidth = 1280;
        public const int mBackBufferHeight = 720;

        public PhareAwayGame()
        {
            _mGraphics = new GraphicsDeviceManager(this);
            _mGraphics.PreferredBackBufferWidth = mBackBufferWidth;
            _mGraphics.PreferredBackBufferHeight = mBackBufferHeight;

            Content.RootDirectory = "Resources";

            
        }

        protected override void LoadContent()
        {
            _mSpriteBatch = new SpriteBatch(GraphicsDevice);
            _mContent = new ContentManager(Services, "Resources");

            _mLevelMain = new LevelMain(this, _mContent);
            _mLevelMain.Init();
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.Singleton.IsKeyPressed(Keys.Escape))
                Exit();

            base.Update(gameTime);

            InputManager.Singleton.Update();

            float Dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            _mLevelMain.Update(Dt);
            SceneManager.Singleton.Update(Dt);
        }

        protected override void Draw(GameTime gameTime)
        {
            _mSpriteBatch.GraphicsDevice.Clear(Color.DeepPink);

            SceneManager.Singleton.Draw(_mSpriteBatch, _mGraphics);

            base.Draw(gameTime);
        }
    }
}
