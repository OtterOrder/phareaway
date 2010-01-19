using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Text;

namespace PhareAway
{
    class Level
    {
        private Texture2D background;

        // Level content.        
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Resources");

            background = Content.Load<Texture2D>("Graphics/Backgrounds/bg_Space_01");

        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            graphics.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            graphics.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

            Rectangle source = new Rectangle(0, 0, background.Width * 20, background.Height * 10);

            spriteBatch.Draw(background, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);

            spriteBatch.End();
        }

    }
}
