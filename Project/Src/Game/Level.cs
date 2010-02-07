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

        private UInt32 _mSceneOutside;
        private UInt32 _mSceneInside;

        private Camera _mCamArchi;
        private Camera _mCamPhilo;

        // Level content.        
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level(IServiceProvider serviceProvider, PhareAwayGame _Game)
        {
            content = new ContentManager(serviceProvider, "Resources");

            _mSceneInside = SceneManager.Singleton.CreateScene();

            _mCamArchi = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamArchi.SetViewportParam(0, 0, 426, 720);
            _mCamPhilo = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamPhilo.SetViewportParam(852, 0, 426, 720);

            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", Content, _mSceneInside);
            Bg.Depth = 0.5f;
            Bg._mSpeed.Y = -0.05f;

            _mArchi = new Character();

            // Ground
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition = new Vector2(100.0f, 400.0f);
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width) /2.0f;
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.Y = 500.0f;
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            //
            bgDecor = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Background", Content, _mSceneInside);
            bgDecor.Depth = 0.4f;
            bgDecor.mPosition.X = 100.0f;
            bgDecor.mPosition.Y = 100.0f;

            //-----------------
            // Init Player
            CharacterParameters Params = new CharacterParameters();
            Params.mFileBase = "Graphics/Sprites/Inside/Characters/Archi/";

            Params.mSpritesParams[0].mFileName = "Archi_Idle";

            Params.mSpritesParams[1].mFileName = "Archi_Walk";
            Params.mSpritesParams[1].mNbFrames = 4;
            Params.mSpritesParams[1].mFps = 15.0f;
            Params.mSpritesParams[1].mLoop = true;

            Params.mSpritesParams[2].mFileName = "Archi_Jump";

            Params.mSpritesParams[3].mFileName = "Archi_Fall";

            _mArchi.Init(Content, Params, _mSceneInside);
        }

        public void Update(float _Dt)
        {
            _mArchi.Update(_Dt);
            _mCamArchi._mFocus = _mArchi.GetPosition();
            _mCamPhilo._mFocus = _mArchi.GetPosition();
        }
    }
}
