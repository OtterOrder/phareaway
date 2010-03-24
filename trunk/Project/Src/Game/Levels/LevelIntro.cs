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
    public class LevelIntro : Level
    {
        private PhareAwayGame _mGame;

        private Sprite _mSprIntro;

        private UInt32 _mSceneIntro;
        private Camera _mDefaultCam;

        public LevelIntro(PhareAwayGame _Game, ContentManager _Content)
        : base(_Game, _Content)
        {
            _mSceneIntro = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneIntro);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mSprIntro = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Intro/Intro_0", _mContent, _mSceneIntro);
            _mSprIntro.mPosition = new Vector2(0.0f, 0.0f);
            _mSprIntro.mOrigin = new Vector2(0.0f, 0.0f);

        }

        public override void Update(float _Dt)
        {
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                _mGame.ChangeLevel(LevelName.Level_Main);
            }
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneIntro);
        }

        public override void InitSound()
        {
        }
    }
}