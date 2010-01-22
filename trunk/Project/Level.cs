using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    class Level
    {
        private Texture2D background;
        private Sprite Spr;

        private float speed;
        private float posY;

        KeyboardState CurKeyboardState;
        KeyboardState LastKeyboardState;

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

            Spr = new Sprite("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk", Content, 4, 15);
            Spr._mDepth = 0.9f;
            Spr._mPosition.X = 205.42f;
            Spr._mPosition.Y = 105.42f;
            if (Spr.AnimPlayer != null)
            {
                Spr.AnimPlayer.Loop = true;
                Spr.AnimPlayer.Speed = 1.0f;
            }
        }

        public void Update(GameTime gameTime)
        {
            posY += speed * (float)gameTime.ElapsedGameTime.TotalMilliseconds;

            Spr.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            ////. Test
            /*
            CurKeyboardState = Keyboard.GetState();

            if (CurKeyboardState.IsKeyDown(Keys.Up) && LastKeyboardState.IsKeyUp(Keys.Up) && Spr.AnimPlayer != null)
                Spr.AnimPlayer.CurrentFrame = Spr.AnimPlayer.CurrentFrame + 1;
            else
            if (CurKeyboardState.IsKeyDown(Keys.Down) && LastKeyboardState.IsKeyUp(Keys.Down) && Spr.AnimPlayer != null)
                Spr.AnimPlayer.CurrentFrame = Spr.AnimPlayer.CurrentFrame - 1;

            if (CurKeyboardState.IsKeyDown(Keys.Enter) && LastKeyboardState.IsKeyUp(Keys.Enter) && Spr.AnimPlayer != null)
                Spr.AnimPlayer.Pause = !Spr.AnimPlayer.Pause;

            LastKeyboardState = CurKeyboardState;
            */
            ////.
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            graphics.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            graphics.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

            graphics.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Point;
            graphics.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point;

            Rectangle source = new Rectangle(0, (int)posY, background.Width * 20, background.Height * 10);

            spriteBatch.Draw(background, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);

            //-----------------------
            Spr.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}
