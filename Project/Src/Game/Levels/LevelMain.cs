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
        private PhareAwayGame _mGame;
        private Background Bg;
        private Sprite Spr, bgDecor;

        private Sprite[] _mMenuSprite;
        private Sprite[] _mMenuSpriteSelect;

        private int _mIDChoice = 0;

        private Character _mArchi, _mPhilo;
        private Lighthouse _mLighthouse;

        private UInt32 _mSceneInside;
        private UInt32 _mSceneOutside;
        private UInt32 _mScenePause;

        private Camera _mCamArchi;
        private Camera _mCamPhilo;
        private Camera _mCamOutside;
        private Camera _mDefaultCam;

        private bool _mPaused = false;

        public LevelMain(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            // Scènes 
            _mSceneInside  = SceneManager.Singleton.CreateScene();
            _mSceneOutside = SceneManager.Singleton.CreateScene();
            _mScenePause = SceneManager.Singleton.CreateScene();

            _mGame = _Game;
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

            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mScenePause);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            InitDecorInside();
            InitDecorOutside();
            InitPlayers();
            InitGameObject();
            InitPauseMenu();
        }

        public override void Update(float _Dt)
        {
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Escape))
            {
                _mPaused = !_mPaused;
            }

            if (!_mPaused)
            {
                _mArchi.Update(_Dt);
                _mPhilo.Update(_Dt);

                ObstacleManager.Singleton.Update(_Dt);

                _mLighthouse.Update(_Dt);
                FlashManager.Singleton.Update(_Dt);

                MachineManager.Singleton.Update(_Dt);


                _mCamArchi.mFocus = _mArchi.GetPosition();
                _mCamPhilo.mFocus = _mPhilo.GetPosition();
            }
            else
                UpdatePauseMenu();
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            if(!_mPaused)
            {
                SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneInside);
                SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneOutside);
            }
            else
                SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mScenePause);

        }

        private void InitDecorInside()
        {
            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", _mContent, _mSceneInside);
            Bg.Depth = 0.9f;
            Bg._mSpeed.Y = -0.05f;

            //Gates
            GateManager.Singleton.AddGate("Graphics/Sprites/Inside/Decor/Door_2", _mContent, _mSceneInside, new Vector2(230, 460), new Vector2(447, 1121));
            GateManager.Singleton.AddGate("Graphics/Sprites/Inside/Decor/Door_3", _mContent, _mSceneInside, new Vector2(230, 701), new Vector2(150, 1001));
            GateManager.Singleton.AddGate("Graphics/Sprites/Inside/Decor/Door_1", _mContent, _mSceneInside, new Vector2(650, 1001), new Vector2(150, 1241));

            // Ladder
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(580.0f, 1000.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(596.0f, 1240.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1240.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1120.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(225.0f, 1000.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(485.0f, 880.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(485.0f, 821.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(314.0f, 700.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Ladder", _mContent, _mSceneInside);
            Spr.Depth = 0.5f;
            Spr.mPosition = new Vector2(314.0f, 580.0f);
            Spr.mOrigin = new Vector2((float)Spr.Width / 2.0f, (float)Spr.Height);
            Spr.SetBoundingBox((UInt32)CollisionId.Ladder, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            // Ground
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1240.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1120.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 1000.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 110.0f;
            Spr.mPosition.Y = 880.0f;
            Spr.mScale.X = (581);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 700.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 579.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 459.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Ground", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 190.0f;
            Spr.mPosition.Y = 280.0f;
            Spr.mScale.X = (420);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            //Wall
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 100.0f;
            Spr.mPosition.Y = 889.0f;
            Spr.mScale.Y = (352);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 690.0f;
            Spr.mPosition.Y = 889.0f;
            Spr.mScale.Y = (352);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 180.0f;
            Spr.mPosition.Y = 289.0f;
            Spr.mScale.Y = (592);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 609.0f;
            Spr.mPosition.Y = 289.0f;
            Spr.mScale.Y = (592);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            // Wall (intérieur)
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 620.0f;
            Spr.mPosition.Y = 1130.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 620.0f;
            Spr.mPosition.Y = 1120.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 395.0f;
            Spr.mPosition.Y = 1010.0f;
            Spr.mScale.Y = (110);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

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
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 250.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 540.0f;
            Spr.mPosition.Y = 890.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 540.0f;
            Spr.mPosition.Y = 880.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 451.0f;
            Spr.mPosition.Y = 710.0f;
            Spr.mScale.Y = (82);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 451.0f;
            Spr.mPosition.Y = 700.0f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Wall", _mContent, _mSceneInside);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 510.0f;
            Spr.mPosition.Y = 589.0f;
            Spr.mScale.Y = (42);
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Corner_HorDown", _mContent, _mSceneInside);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 510.0f;
            Spr.mPosition.Y = 579.0f;


            //Lab 
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Collisions/Lab", _mContent, _mSceneInside);
            Spr.Depth = 0.51f;
            Spr.mPosition.X = 189.0f;
            Spr.mPosition.Y = 469.0f;
            Spr.SetBoundingBox((UInt32)CollisionId.Ground, Vector2.Zero, new Vector2(Spr.Width, Spr.Height));


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


            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Lightnings", _mContent, _mSceneInside, 6, 5.0f);
            Spr.Depth = 0.6f;
            Spr.mPosition.X = 294.0f;
            Spr.mPosition.Y = 736.0f;
            Spr.AnimPlayer.Loop = true;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/FeedingTray", _mContent, _mSceneInside, 4, 10.0f);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 239.0f;
            Spr.mPosition.Y = 1197.0f;
            Spr.AnimPlayer.Loop = true;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/FeedingTray", _mContent, _mSceneInside, 4, 10.0f);
            Spr.Depth = 0.38f;
            Spr.mPosition.X = 423.0f;
            Spr.mPosition.Y = 1197.0f;
            Spr.AnimPlayer.Loop = true;
            Spr.mFlip = SpriteEffects.FlipHorizontally;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Hamster", _mContent, _mSceneInside, 4, 10.0f);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 266.0f;
            Spr.mPosition.Y = 1255.0f;
            Spr.AnimPlayer.Loop = true;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Decor/Hamster", _mContent, _mSceneInside, 4, 10.0f);
            Spr.Depth = 0.39f;
            Spr.mPosition.X = 430.0f;
            Spr.mPosition.Y = 1255.0f;
            Spr.AnimPlayer.Loop = true;
            Spr.AnimPlayer.CurrentFrame = 1;
            Spr.mFlip = SpriteEffects.FlipHorizontally;

            // Machines
            MachineParameters MParams;

            MParams = new MachineParameters();
            MParams.mId = MachineId.Pipes;
            MParams.mPosition = new Vector2(364.0f, 932.0f);
            MParams.mSprite.mFileName = "Graphics/Sprites/Inside/Machines/Pipes";
            MParams.mSprite.mNbFrames = 11;
            MParams.mDepth = 0.6f;
            MParams.mBBoxSize = new Vector2(57.0f, 34.0f);
            MachineManager.Singleton.AddMachine(_mContent, _mSceneInside, MParams);

            MParams = new MachineParameters();
            MParams.mId = MachineId.Zeus;
            MParams.mPosition = new Vector2(309.0f, 730.0f);
            MParams.mSprite.mFileName = "Graphics/Sprites/Inside/Machines/Zeus";
            MParams.mSprite.mNbFrames = 11;
            MParams.mDepth = 0.6f;
            MParams.mBBoxSize = new Vector2(64.0f, 123.0f);
            MParams.mBBoxOffset = new Vector2(-20.5f, 27.0f);
            MachineManager.Singleton.AddMachine(_mContent, _mSceneInside, MParams);

            MParams = new MachineParameters();
            MParams.mId = MachineId.EngineL;
            MParams.mPosition = new Vector2(257.0f, 1254.0f);
            MParams.mSprite.mFileName = "Graphics/Sprites/Inside/Machines/Engine";
            MParams.mSprite.mNbFrames = 13;
            MParams.mDepth = 0.2f;
            MParams.mBBoxSize = new Vector2(49.0f, 33.0f);
            MParams.mBBoxOffset = new Vector2(-18.0f, -57.0f);
            MachineManager.Singleton.AddMachine(_mContent, _mSceneInside, MParams);

            MParams = new MachineParameters();
            MParams.mId = MachineId.EngineR;
            MParams.mPosition = new Vector2(554.0f, 1254.0f);
            MParams.mSprite.mFileName = "Graphics/Sprites/Inside/Machines/Engine";
            MParams.mSprite.mNbFrames = 13;
            MParams.mDepth = 0.2f;
            MParams.mBBoxSize = new Vector2(49.0f, 33.0f);
            MParams.mBBoxOffset = new Vector2(-27.0f, -57.0f);
            MachineManager.Singleton.AddMachine(_mContent, _mSceneInside, MParams);
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
            ArchiParams.mCollisionId = (UInt32)CollisionId.Archi;
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

            ArchiParams.mMachinesSpritesParams[(int)MachineId.Pipes].mFileName = "Machines/Archi_Pipes";
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Pipes].mNbFrames = 4;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Pipes].mFps = 10.0f;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Pipes].mLoop = false;

            ArchiParams.mMachinesSpritesParams[(int)MachineId.Zeus].mFileName = "Machines/Archi_Zeus";
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Zeus].mNbFrames = 8;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Zeus].mFps = 15.0f;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.Zeus].mLoop = false;

            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineL].mFileName = "Machines/Archi_Engine";
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineL].mNbFrames = 9;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineL].mFps = 15.0f;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineL].mLoop = false;

            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineR].mFileName = "Machines/Archi_Engine";
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineR].mNbFrames = 9;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineR].mFps = 15.0f;
            ArchiParams.mMachinesSpritesParams[(int)MachineId.EngineR].mLoop = false;

            ArchiParams.mInputParams.mRight = Keys.D;
            ArchiParams.mInputParams.mLeft = Keys.Q;
            ArchiParams.mInputParams.mUp = Keys.Z;
            ArchiParams.mInputParams.mDown = Keys.S;
            ArchiParams.mInputParams.mJump = Keys.LeftShift;
            ArchiParams.mInputParams.mAction = Keys.E;

            _mArchi.Init(_mContent, ArchiParams, _mSceneInside);

            CharacterParameters PhiloParams = new CharacterParameters();
            PhiloParams.mCollisionId = (UInt32)CollisionId.Philo;
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

            PhiloParams.mMachinesSpritesParams[(int)MachineId.Pipes].mFileName = "Machines/Philo_Pipes";
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Pipes].mNbFrames = 4;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Pipes].mFps = 10.0f;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Pipes].mLoop = false;

            PhiloParams.mMachinesSpritesParams[(int)MachineId.Zeus].mFileName = "Machines/Philo_Zeus";
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Zeus].mNbFrames = 8;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Zeus].mFps = 15.0f;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.Zeus].mLoop = false;

            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineL].mFileName = "Machines/Philo_Engine";
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineL].mNbFrames = 9;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineL].mFps = 15.0f;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineL].mLoop = false;

            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineR].mFileName = "Machines/Philo_Engine";
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineR].mNbFrames = 9;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineR].mFps = 15.0f;
            PhiloParams.mMachinesSpritesParams[(int)MachineId.EngineR].mLoop = false;

            PhiloParams.mInputParams.mRight = Keys.M;
            PhiloParams.mInputParams.mLeft = Keys.K;
            PhiloParams.mInputParams.mUp = Keys.O;
            PhiloParams.mInputParams.mDown = Keys.L;
            PhiloParams.mInputParams.mJump = Keys.N;
            PhiloParams.mInputParams.mAction = Keys.P;

            _mPhilo.Init(_mContent, PhiloParams, _mSceneInside);

        }

        private void InitGameObject()
        {

            _mLighthouse = new Lighthouse();

            LighthouseParameters LHParams = new LighthouseParameters();
            LHParams.mDepth = 0.5f;
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



            FlashesParameters FParams = new FlashesParameters();
            FParams.mDepth = 0.4f;
            FParams.mFileBase = "Graphics/Sprites/Outside/Lighthouse/";
            FParams.mFileName = "Flash";
            FParams.mSpeed = -0.3f;
            FParams.mYMin = 0.0f;

            FlashManager.Singleton.Initialize(FParams, _mContent, _mSceneOutside);


            ObstaclesParameters ObsParams = new ObstaclesParameters(10, 11);
            ObsParams.mFileBase = "Graphics/Sprites/Outside/Obstacles/";
            ObsParams.mDepth = 0.45f;
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
            ObsParams.mExplosionSpr.mFileName = "Explosion";
            ObsParams.mExplosionSpr.mFps = 20.0f;
            ObsParams.mExplosionSpr.mNbFrames = 7;

            ObstacleManager.Singleton.Initialize(ObsParams, _mContent, _mSceneOutside);


            // Game Init
            _mArchi.mActive = false;

            _mLighthouse.mActive = true;
            _mLighthouse.InputParameters = _mArchi.InputParameters;

        }

        private void InitPauseMenu()
        {
            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Backgrounds/Menu_pause", _mContent, _mScenePause);
            Spr.mPosition = new Vector2(0.0f, 0.0f);
            Spr.mOrigin = new Vector2(0.0f, 0.0f);
            Spr.Depth = 0.9f;

            _mMenuSprite = new Sprite[3];

            _mMenuSprite[0] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Resume_0", _mContent, _mScenePause);
            _mMenuSprite[0].mPosition = new Vector2(970.0f, 260.0f);
            _mMenuSprite[0].mOrigin = new Vector2(_mMenuSprite[0].Width, _mMenuSprite[0].Height);
            _mMenuSprite[0].Depth = 0.5f;

            _mMenuSprite[1] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/HowToPlay_0", _mContent, _mScenePause);
            _mMenuSprite[1].mPosition = new Vector2(970.0f, 310.0f);
            _mMenuSprite[1].mOrigin = new Vector2(_mMenuSprite[1].Width, _mMenuSprite[1].Height);
            _mMenuSprite[1].Depth = 0.5f;

            _mMenuSprite[2] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/ExitToMenu_0", _mContent, _mScenePause);
            _mMenuSprite[2].mPosition = new Vector2(970.0f, 346.0f);
            _mMenuSprite[2].mOrigin = new Vector2(_mMenuSprite[2].Width, _mMenuSprite[2].Height);
            _mMenuSprite[2].Depth = 0.5f;

            _mMenuSpriteSelect = new Sprite[3];

            _mMenuSpriteSelect[0] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Resume_1", _mContent, _mScenePause);
            _mMenuSpriteSelect[0].mPosition = new Vector2(970.0f, 260.0f);
            _mMenuSpriteSelect[0].mOrigin = new Vector2(_mMenuSpriteSelect[0].Width, _mMenuSpriteSelect[0].Height);
            _mMenuSpriteSelect[0].mVisible = false;
            _mMenuSpriteSelect[0].Depth = 0.5f;

            _mMenuSpriteSelect[1] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/HowToPlay_1", _mContent, _mScenePause);
            _mMenuSpriteSelect[1].mPosition = new Vector2(970.0f, 310.0f);
            _mMenuSpriteSelect[1].mOrigin = new Vector2(_mMenuSpriteSelect[1].Width, _mMenuSpriteSelect[1].Height);
            _mMenuSpriteSelect[1].mVisible = false;
            _mMenuSpriteSelect[1].Depth = 0.5f;

            _mMenuSpriteSelect[2] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/ExitToMenu_1", _mContent, _mScenePause);
            _mMenuSpriteSelect[2].mPosition = new Vector2(970.0f, 346.0f);
            _mMenuSpriteSelect[2].mOrigin = new Vector2(_mMenuSpriteSelect[2].Width, _mMenuSpriteSelect[2].Height);
            _mMenuSpriteSelect[2].mVisible = false;
            _mMenuSpriteSelect[2].Depth = 0.5f;

        }

        private void UpdatePauseMenu()
        {
            _mMenuSprite[_mIDChoice].mVisible = false;
            _mMenuSpriteSelect[_mIDChoice].mVisible = true;

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Down))
            {
                if (_mIDChoice < 2)
                {
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__HUD__SELECT");
                    _mMenuSprite[_mIDChoice].mVisible = true;
                    _mMenuSpriteSelect[_mIDChoice].mVisible = false;
                    _mIDChoice++;
                }
            }
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Up))
            {
                if (_mIDChoice > 0)
                {
                    SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__HUD__SELECT");
                    _mMenuSprite[_mIDChoice].mVisible = true;
                    _mMenuSpriteSelect[_mIDChoice].mVisible = false;
                    _mIDChoice--;
                }
            }

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                SoundManager.Singleton._mSoundSoundBank.PlayCue("SND__HUD__OK");
                switch (_mIDChoice)
                {
                    case 0: _mPaused = !_mPaused; break;
                    case 1: _mGame.ChangeLevel(LevelName.Level_Tuto); break;
                    case 2: _mGame.ChangeLevel(LevelName.Level_Menu); break;
                }
            }

        }

        public override void InitSound()
        {
        }
    }
}
