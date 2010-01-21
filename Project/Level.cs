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
        private Animation Anim;

        private float speed;
        private float posY;

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

            speed = -0.2f;
            posY = 0;

            Anim = new Animation("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk_", 4, 15, Content);
            Anim.Loop = true;
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            posY += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Anim.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            graphics.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            graphics.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

            Rectangle source = new Rectangle(0, (int)posY, background.Width * 20, background.Height * 10);

            spriteBatch.Draw(background, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);

            //-----------------------------
            source.X = source.Y = 0;
            source.Height = Anim.Height;
            source.Width = Anim.Width;

            Vector2 pos = new Vector2(100.0f, 20.0f);
            spriteBatch.Draw(Anim.CurrentFrame, pos, source, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 0.8f);

            spriteBatch.End();
        }

    }
}
