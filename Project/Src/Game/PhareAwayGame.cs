using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace PhareAway
{
    public class PhareAwayGame : Microsoft.Xna.Framework.Game
    {

        private GraphicsDeviceManager graphics;
        private SpriteBatch spriteBatch;

        private Level level;

        public const int BackBufferWidth = 1280;
        public const int BackBufferHeight = 720;

        public PhareAwayGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;

            Content.RootDirectory = "Resources";
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);

            level = new Level(Services, this);
        }

        protected override void Update(GameTime gameTime)
        {
            if (InputManager.Singleton.IsKeyPressed(Keys.Escape))
                Exit();

            base.Update(gameTime);

            InputManager.Singleton.Update();

            float Dt = (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            level.Update(Dt);
            SceneManager.Singleton.Update(Dt);
        }

        protected override void Draw(GameTime gameTime)
        {
            graphics.GraphicsDevice.Clear(Color.DeepPink);

            SceneManager.Singleton.Draw(spriteBatch, graphics);

            base.Draw(gameTime);
        }
    }
}
