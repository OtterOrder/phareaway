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
    public class Level
    {
        private Background Bg;
        private Sprite Spr, bgDecor;
        private Character _mArchi;

        // Level content.        
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level(IServiceProvider serviceProvider, PhareAwayGame _Game)
        {
            content = new ContentManager(serviceProvider, "Resources");

            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", Content);
            Bg.Depth = 0.5f;
            Bg._mSpeed.Y = -0.05f;

            _mArchi = new Character();

            // Ground
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content);
            Spr.Depth = 0.39f;
            Spr.mPosition = new Vector2(100.0f, 400.0f);
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width) /2.0f;
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content);
            Spr.Depth = 0.39f;
            Spr.mPosition.Y = 500.0f;
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            //
            bgDecor = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Background", Content);
            bgDecor.Depth = 0.4f;
            bgDecor.mPosition.X = 100.0f;
            bgDecor.mPosition.Y = 100.0f;

            //_mPlayer = new Player(_Game);
            _mArchi.Init(Content);
        }

        public void Update(float _Dt)
        {
            _mArchi.Update(_Dt);
        }
    }
}
