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
    public class LevelMain : Level
    {
        private Background Bg;
        private Sprite Spr, bgDecor;

        private Character _mArchi, _mPhilo;
        private Lighthouse _mLighthouse;

        private UInt32 _mSceneInside;
        private UInt32 _mSceneOutside;

        private Camera _mCamArchi;
        private Camera _mCamPhilo;
        private Camera _mCamOutside;

        public LevelMain(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            // Scènes 
            _mSceneInside  = SceneManager.Singleton.CreateScene();
            _mSceneOutside = SceneManager.Singleton.CreateScene();
        }

        public override void Init()
        {
            // Caméras
            _mCamArchi = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamArchi.SetViewportParam(0, 0, 0.34f, 1.0f);
            _mCamPhilo = SceneManager.Singleton.GetNewCamera(_mSceneInside);
            _mCamPhilo.SetViewportParam(0.66f, 0, 0.341f, 1.0f);

            _mCamOutside = SceneManager.Singleton.GetNewCamera(_mSceneOutside);
            _mCamOutside.SetViewportParam(0.34f, 0, 0.32f, 1.0f);
            _mCamOutside.Position = new Vector2(212.0f, 360.0f);

            InitDecorInside();
            InitDecorOutside();
            InitPlayers();
            InitGameObject();
        }

        public override void Update(float _Dt)
        {
            ObstacleManager.Singleton.Update(_Dt);

            _mArchi.Update(_Dt);
            _mPhilo.Update(_Dt);

            _mLighthouse.Update(_Dt);

            _mCamArchi.mFocus = _mArchi.GetPosition();
            _mCamPhilo.mFocus = _mPhilo.GetPosition();
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneInside);
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneOutside);
        }

        private void InitDecorInside()
        {
            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", _mContent, _mSceneInside);
            Bg.Depth = 0.9f;
            Bg._mSpeed.Y = -0.05f;


            // Ladder
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(580.0f, 1000.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(596.0f, 1240.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1240.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1120.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1000.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(485.0f, 880.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(485.0f, 821.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(314.0f, 700.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(314.0f, 580.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox(3, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            // Ground
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1240.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1120.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1000.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 880.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 700.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 579.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 459.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 280.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            //Wall
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 889.0f;
            Spr.mScale.Y = (352);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 889.0f;
            Spr.mScale.Y = (352);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 289.0f;
            Spr.mScale.Y = (592);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 289.0f;
            Spr.mScale.Y = (592);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            // Wall (intérieur)
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 620.0f;
            Spr.mPosition.Y = 1130.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 620.0f;
            Spr.mPosition.Y = 1120.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 395.0f;
            Spr.mPosition.Y = 1010.0f;
            Spr.mScale.Y = (110);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 395.0f;
            Spr.mPosition.Y = 1000.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorUp", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 395.0f;
            Spr.mPosition.Y = 1120.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 250.0f;
            Spr.mPosition.Y = 890.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 250.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 540.0f;
            Spr.mPosition.Y = 890.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 540.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 451.0f;
            Spr.mPosition.Y = 710.0f;
            Spr.mScale.Y = (82);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 451.0f;
            Spr.mPosition.Y = 700.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 510.0f;
            Spr.mPosition.Y = 589.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 510.0f;
            Spr.mPosition.Y = 579.0f;


            //Lab 
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Lab", _mContent, _mSceneInside);
            Spr.Depth = 0.51f;
            Spr.mPosition.X = 189.0f;
            Spr.mPosition.Y = 469.0f;
            Spr.SetBoundingBox(2, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));


            //Corners
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 1240.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 1120.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 1000.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_UpR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 700.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 579.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 459.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_UpR", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 280.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 1240.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 1120.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 1000.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_UpL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 700.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 579.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_vertL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 459.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_UpL", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 280.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorUp", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorUp", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorUp", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 310.0f;
            Spr.mPosition.Y = 280.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorUp", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 480.0f;
            Spr.mPosition.Y = 280.0f;

            bgDecor = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Background", _mContent, _mSceneInside);
            bgDecor.Depth = 0.7f;
            bgDecor.mPosition.X = 100.0f;
            bgDecor.mPosition.Y = 100.0f;

        }

        private void InitDecorOutside()
        {
            // Outside
            SceneManager.Singleton.GetScene(_mSceneOutside).AddBackground(Bg);

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Outside/Splitter", _mContent, _mSceneOutside);
            Spr.Depth = 0.1f;
            Spr.mPosition = new Vector2(8.0f, 0.0f);
            Spr.mScale = new Vector2(2.0f, 720.0f);

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Outside/Splitter", _mContent, _mSceneOutside);
            Spr.Depth = 0.1f;
            Spr.mPosition = new Vector2(415.0f, 0.0f);
            Spr.mScale = new Vector2(2.0f, 720.0f);

        }

        private void InitPlayers()
        {
            _mArchi = new Character();
            _mArchi.SetPosition(new Vector2(150, 1050));
            _mPhilo = new Character();
            _mPhilo.SetPosition(new Vector2(289, 646));

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

            ArchiParams.mSpritesParams[4].mFileName = "Archi_Climb";
            ArchiParams.mSpritesParams[4].mNbFrames = 4;
            ArchiParams.mSpritesParams[4].mFps = 15.0f;
            ArchiParams.mSpritesParams[4].mLoop = true;

            ArchiParams.mInputParams.mRight = Keys.D;
            ArchiParams.mInputParams.mLeft = Keys.Q;
            ArchiParams.mInputParams.mUp = Keys.Z;
            ArchiParams.mInputParams.mDown = Keys.S;
            ArchiParams.mInputParams.mJump = Keys.LeftShift;

            _mArchi.Init(_mContent, ArchiParams, _mSceneInside);

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

            PhiloParams.mSpritesParams[4].mFileName = "Philo_Climb";
            PhiloParams.mSpritesParams[4].mNbFrames = 4;
            PhiloParams.mSpritesParams[4].mFps = 15.0f;
            PhiloParams.mSpritesParams[4].mLoop = true;

            PhiloParams.mInputParams.mRight = Keys.M;
            PhiloParams.mInputParams.mLeft = Keys.K;
            PhiloParams.mInputParams.mUp = Keys.O;
            PhiloParams.mInputParams.mDown = Keys.L;
            PhiloParams.mInputParams.mJump = Keys.N;

            _mPhilo.Init(_mContent, PhiloParams, _mSceneInside);

        }

        private void InitGameObject()
        {

            _mLighthouse = new Lighthouse();

            LighthouseParameters LHParams = new LighthouseParameters();
            LHParams.mDepth = 0.3f;
            LHParams.mFileBase = "Graphics/Sprites/Outside/Lighthouse/";

            LHParams.mSprLightouse.mFileName = "Lighthouse";

            LHParams.mSprJetEngine.mFileName = "JetEngine";
            LHParams.mSprJetEngine.mNbFrames = 3;
            LHParams.mSprJetEngine.mFps = 5.0f;
            LHParams.mSprJetEngine.mLoop = true;

            LHParams.mGameParams.mXMin = 30.0f;
            LHParams.mGameParams.mYMin = 136.0f;
            LHParams.mGameParams.mXMax = 394.0f;
            LHParams.mGameParams.mYMax = 710.0f;

            _mLighthouse.SetPosition(new Vector2(212.0f, 700.0f));

            _mLighthouse.Init(_mContent, LHParams, _mSceneOutside);


            ObstaclesParameters ObsParams = new ObstaclesParameters(10, 11);
            ObsParams.mFileBase = "Graphics/Sprites/Outside/Obstacles/";
            ObsParams.mSprites[0] = "Obstacle_01";
            ObsParams.mSprites[1] = "Obstacle_02";
            ObsParams.mSprites[2] = "Obstacle_03";
            ObsParams.mSprites[3] = "Obstacle_04";
            ObsParams.mSprites[4] = "Obstacle_05";
            ObsParams.mSprites[5] = "Obstacle_06";
            ObsParams.mSprites[6] = "Obstacle_07";
            ObsParams.mSprites[7] = "Obstacle_08";
            ObsParams.mSprites[8] = "Obstacle_09";
            ObsParams.mSprites[9] = "Obstacle_10";
            ObsParams.mSprites[10] = "Obstacle_11";
            ObsParams.mXRange = new Vector2(0.0f, 0.32f * PhareAwayGame.mBackBufferWidth);
            ObsParams.mYInit = -10.0f;
            ObsParams.mYMax = (float)PhareAwayGame.mBackBufferHeight + 20.0f;
            ObsParams.mSpeedA = 0.1f;

            ObstacleManager.Singleton.Initialize(ObsParams, _mContent, _mSceneOutside);


            // Game Init
            _mArchi.mActive = false;

            _mLighthouse.mActive = true;
            _mLighthouse.InputParameters = _mArchi.InputParameters;

        }
    }
}
