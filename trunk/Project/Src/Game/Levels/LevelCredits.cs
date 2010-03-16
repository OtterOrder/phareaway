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
    public class LevelCredits : Level
    {
        private PhareAwayGame _mGame;

        private Sprite _mSprCredits;

        private UInt32 _mSceneCredits;
        private Camera _mDefaultCam;

        public LevelCredits(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            _mSceneCredits = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneCredits);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mSprCredits = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Credits2", _mContent, _mSceneCredits);
            _mSprCredits.mPosition = new Vector2(128.0f, 0.0f);
            _mSprCredits.mOrigin = new Vector2(0.0f, 0.0f);

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
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneCredits);
        }
    }
}
