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

        private Background Bg;
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

            Bg = new Background("Graphics/Backgrounds/bg_Space_01", Content);
            Bg._mSpeed.Y = -50.0f;


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
            Bg.Update((float)gameTime.ElapsedGameTime.TotalSeconds);
            Spr.Update((float)gameTime.ElapsedGameTime.TotalSeconds);

            ////. Test
            CurKeyboardState = Keyboard.GetState();

            if (CurKeyboardState.IsKeyDown(Keys.Right) /*&& LastKeyboardState.IsKeyUp(Keys.Right)*/)
            {
                Spr._mPosition.X += 2.0f;
            }
            else
            if (CurKeyboardState.IsKeyDown(Keys.Left) /*&& LastKeyboardState.IsKeyUp(Keys.Left)*/)
            {
                Spr._mPosition.X -= 2.0f;
            }


            if (CurKeyboardState.IsKeyDown(Keys.Up))
            {
                Spr._mPosition.Y -= 2.0f;
            }
            else
            if (CurKeyboardState.IsKeyDown(Keys.Down))
            {
                Spr._mPosition.Y += 2.0f;
            }

            LastKeyboardState = CurKeyboardState;
            ////.
        }

        public void Draw(GameTime gameTime, SpriteBatch spriteBatch, GraphicsDeviceManager graphics)
        {
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            graphics.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            graphics.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;
            
            graphics.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Linear;
            graphics.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Linear;
            /*
            Rectangle source = new Rectangle(0, (int)posY, background.Width * 20, background.Height * 10);

            spriteBatch.Draw(background, Vector2.Zero, source, Color.White, 0, Vector2.Zero, 0.5f, SpriteEffects.None, 1.0f);
            */

            Bg.Draw(spriteBatch);

            spriteBatch.End();

            //-----------------------
            spriteBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            graphics.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Clamp;
            graphics.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Clamp;

            graphics.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Point;
            graphics.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point;

            Spr.Draw(spriteBatch);

            spriteBatch.End();
        }

    }
}
