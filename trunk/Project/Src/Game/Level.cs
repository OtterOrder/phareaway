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
        private Sprite Spr2, bgDecor;
        private Player _mPlayer;

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


            Spr2 = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk", Content);
            Spr2.Depth = 0.07f;
            Spr2._mPosition.X = 100.0f;
            Spr2._mPosition.Y = 100.0f;
            Spr2.SetBoundingBox(1, new Vector2(0.0f, 0.0f), new Vector2(Spr2.Width, Spr2.Height));

            bgDecor = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Background", Content);
            bgDecor.Depth = 0.4f;
            bgDecor._mPosition.X = 100.0f;
            bgDecor._mPosition.Y = 100.0f;

            _mPlayer = new Player(_Game);
        }

        public void Update(float _Dt)
        {
           
        }
    }
}
