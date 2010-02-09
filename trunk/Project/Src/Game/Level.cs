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

        private Character _mArchi, _mPhilo;

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

            // Scènes 
            _mSceneInside = SceneManager.Singleton.CreateScene();

            // Caméras
            _mCamArchi = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamArchi.SetViewportParam(0, 0, 428, 720);
            _mCamPhilo = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamPhilo.SetViewportParam(852, 0, 428, 720);

            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", Content, _mSceneInside);
            Bg.Depth = 0.9f;
            Bg._mSpeed.Y = -0.05f;

            _mArchi = new Character();
            _mArchi.SetPosition(new Vector2(150, 900));
            _mPhilo = new Character();
            _mPhilo.SetPosition(new Vector2(450, 900));

            // Ground
            /*Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition = new Vector2(100.0f, 400.0f);
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width) /2.0f;
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition = new Vector2(400.0f, 480.0f);
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width) / 42.0f;
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.Y = 500.0f;
            Spr.mScale.X = ((float)PhareAwayGame.BackBufferWidth / (float)Spr.Width);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));*/

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1000.0f;
            Spr.mScale.X = (20);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1120.0f;
            Spr.mScale.X = (20);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", Content, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1240.0f;
            Spr.mScale.X = (20);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", Content, _mSceneInside);
            Spr.Depth = 0.40f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 1240.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", Content, _mSceneInside);
            Spr.Depth = 0.40f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 1240.0f;

            //
            bgDecor = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Background", Content, _mSceneInside);
            bgDecor.Depth = 0.7f;
            bgDecor.mPosition.X = 100.0f;
            bgDecor.mPosition.Y = 100.0f;

            //-----------------
            // Init Players
            CharacterParameters ArchiParams = new CharacterParameters();
            ArchiParams.mDepth = 0.4f;
            ArchiParams.mFileBase = "Graphics/Sprites/Inside/Characters/Archi/";

            ArchiParams.mSpritesParams[0].mFileName = "Archi_Idle";

            ArchiParams.mSpritesParams[1].mFileName = "Archi_Walk";
            ArchiParams.mSpritesParams[1].mNbFrames = 4;
            ArchiParams.mSpritesParams[1].mFps = 15.0f;
            ArchiParams.mSpritesParams[1].mLoop = true;

            ArchiParams.mSpritesParams[2].mFileName = "Archi_Jump";

            ArchiParams.mSpritesParams[3].mFileName = "Archi_Fall";

            ArchiParams.mInputParams.mRight = Keys.D;
            ArchiParams.mInputParams.mLeft  = Keys.Q;
            ArchiParams.mInputParams.mUp    = Keys.Z;

            _mArchi.Init(Content, ArchiParams, _mSceneInside);

            CharacterParameters PhiloParams = new CharacterParameters();
            PhiloParams.mDepth = 0.41f;
            PhiloParams.mFileBase = "Graphics/Sprites/Inside/Characters/Philo/";

            PhiloParams.mSpritesParams[0].mFileName = "Philo_Idle";

            PhiloParams.mSpritesParams[1].mFileName = "Philo_Walk";
            PhiloParams.mSpritesParams[1].mNbFrames = 4;
            PhiloParams.mSpritesParams[1].mFps = 15.0f;
            PhiloParams.mSpritesParams[1].mLoop = true;

            PhiloParams.mSpritesParams[2].mFileName = "Philo_Jump";

            PhiloParams.mSpritesParams[3].mFileName = "Philo_Fall";

            PhiloParams.mInputParams.mRight = Keys.M;
            PhiloParams.mInputParams.mLeft  = Keys.K;
            PhiloParams.mInputParams.mUp    = Keys.O;

            _mPhilo.Init(Content, PhiloParams, _mSceneInside);
        }

        public void Update(float _Dt)
        {
            _mArchi.Update(_Dt);
            _mPhilo.Update(_Dt);

            _mCamArchi.mFocus = _mArchi.GetPosition();
            _mCamPhilo.mFocus = _mPhilo.GetPosition();
        }
    }
}
