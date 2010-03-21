using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    public class LevelLogos : Level
    {
        private PhareAwayGame _mGame;
        private Sprite _mLogo1;
        private Sprite _mLogo2;

        private UInt32 _mSceneLogos;
        private Camera _mDefaultCam;

        private float  _mLogoTime = 3.0f;

        public LevelLogos(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            _mSceneLogos = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneLogos);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mLogo1 = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/LOGO_ENJMIN", _mContent, _mSceneLogos);
            _mLogo1.mPosition = new Vector2(0.0f, 0.0f);
            _mLogo1.mOrigin = new Vector2(0.0f, 0.0f);

            _mLogo2 = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/LOGO_SPRAITE", _mContent, _mSceneLogos);
            _mLogo2.mPosition = new Vector2(0.0f, 0.0f);
            _mLogo2.mOrigin = new Vector2(0.0f, 0.0f);
            _mLogo2.mVisible = false;

            Timer.Singleton.Initialize();

        }

        public override void Update(float _Dt)
        {

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Space) || InputManager.Singleton.IsKeyJustPressed(Keys.Escape))
            {
                ChangeLogo();
            }

            if(Timer.Singleton.Seconds > _mLogoTime)
            {
                ChangeLogo();
                Timer.Singleton.Initialize();
            }

        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneLogos);
        }

        private void ChangeLogo()
        {
            if (_mLogo2.mVisible == true)
            {
                _mGame.ChangeLevel(LevelName.Level_Menu);
            }
            else
            {
                _mLogo1.mVisible = false;
                _mLogo2.mVisible = true;
            }

        }
    }
}