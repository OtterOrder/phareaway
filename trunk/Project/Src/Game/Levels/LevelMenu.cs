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
    public class LevelMenu : Level
    {
        private PhareAwayGame _mGame;
        private Sprite _mBgMenu;

        private UInt32 _mSceneMenu;
        private Camera _mDefaultCam;

        public LevelMenu(PhareAwayGame _Game, ContentManager _Content) : base(_Game, _Content)
        {
            _mSceneMenu = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            _mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneMenu);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mBgMenu = SceneManager.Singleton.GetNewSprite("Graphics/Backgrounds/bg_Menu", _mContent, _mSceneMenu);
            _mBgMenu.mPosition = new Vector2(0.0f, 0.0f);
            _mBgMenu.mOrigin = new Vector2(0.0f, 0.0f);

        }

        public override void Update(float _Dt)
        {
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Space) || InputManager.Singleton.IsKeyJustPressed(Keys.Escape))
            {
                _mGame.ChangeLevel(LevelName.Level_Main);
            }

        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneMenu);
        }
    }
}
