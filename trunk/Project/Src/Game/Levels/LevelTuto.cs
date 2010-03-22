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
    public class LevelTuto : Level
    {
        private PhareAwayGame _mGame;

        private Sprite _mSprTuto;
        private Sprite _mCenter;

        private UInt32 _mSceneTuto;
        private Camera _mDefaultCam;

        public LevelTuto(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            _mSceneTuto = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneTuto);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mSprTuto = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Tutorial/Tuto_bg", _mContent, _mSceneTuto);
            _mSprTuto.mPosition = new Vector2(0.0f, 0.0f);
            _mSprTuto.mOrigin = new Vector2(0.0f, 0.0f);

            _mCenter = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Tutorial/Phare_neutre", _mContent, _mSceneTuto);
            _mCenter.mPosition = new Vector2(0.0f, 0.0f);
            _mCenter.mOrigin = new Vector2(0.0f, 0.0f);

        }

        public override void Update(float _Dt)
        {
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                _mGame.ChangeLevel(LevelName.Level_Menu);
            }
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneTuto);
        }
    }
}
