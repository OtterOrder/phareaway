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
        private Sprite [] _mMenuSprite;
        private Sprite [] _mMenuSpriteSelect;

        private UInt32 _mSceneMenu;
        private Camera _mDefaultCam;

        private int _mIDChoice = 0;

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

            _mMenuSprite = new Sprite[4];

            _mMenuSprite[0] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/NewGame_0", _mContent, _mSceneMenu);
            _mMenuSprite[0].mPosition = new Vector2(970.0f, 260.0f);
            _mMenuSprite[0].mOrigin = new Vector2(_mMenuSprite[0].Width, _mMenuSprite[0].Height);

            _mMenuSprite[1] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/HowToPlay_0", _mContent, _mSceneMenu);
            _mMenuSprite[1].mPosition = new Vector2(970.0f, 310.0f);
            _mMenuSprite[1].mOrigin = new Vector2(_mMenuSprite[1].Width, _mMenuSprite[1].Height);

            _mMenuSprite[2] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Credits_0", _mContent, _mSceneMenu);
            _mMenuSprite[2].mPosition = new Vector2(970.0f, 346.0f);
            _mMenuSprite[2].mOrigin = new Vector2(_mMenuSprite[2].Width, _mMenuSprite[2].Height);

            _mMenuSprite[3] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Exit_0", _mContent, _mSceneMenu);
            _mMenuSprite[3].mPosition = new Vector2(970.0f, 386.0f);
            _mMenuSprite[3].mOrigin = new Vector2(_mMenuSprite[3].Width, _mMenuSprite[3].Height);

            _mMenuSpriteSelect = new Sprite[4];

            _mMenuSpriteSelect[0] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/NewGame_1", _mContent, _mSceneMenu);
            _mMenuSpriteSelect[0].mPosition = new Vector2(970.0f, 260.0f);
            _mMenuSpriteSelect[0].mOrigin = new Vector2(_mMenuSpriteSelect[0].Width, _mMenuSpriteSelect[0].Height);
            _mMenuSpriteSelect[0].mVisible = false;

            _mMenuSpriteSelect[1] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/HowToPlay_1", _mContent, _mSceneMenu);
            _mMenuSpriteSelect[1].mPosition = new Vector2(970.0f, 310.0f);
            _mMenuSpriteSelect[1].mOrigin = new Vector2(_mMenuSpriteSelect[1].Width, _mMenuSpriteSelect[1].Height);
            _mMenuSpriteSelect[1].mVisible = false;

            _mMenuSpriteSelect[2] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Credits_1", _mContent, _mSceneMenu);
            _mMenuSpriteSelect[2].mPosition = new Vector2(970.0f, 346.0f);
            _mMenuSpriteSelect[2].mOrigin = new Vector2(_mMenuSpriteSelect[2].Width, _mMenuSpriteSelect[2].Height);
            _mMenuSpriteSelect[2].mVisible = false;

            _mMenuSpriteSelect[3] = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Menu/Exit_1", _mContent, _mSceneMenu);
            _mMenuSpriteSelect[3].mPosition = new Vector2(970.0f, 386.0f);
            _mMenuSpriteSelect[3].mOrigin = new Vector2(_mMenuSpriteSelect[3].Width, _mMenuSpriteSelect[3].Height);
            _mMenuSpriteSelect[3].mVisible = false;
        }

        public override void Update(float _Dt)
        {
            _mMenuSprite[_mIDChoice].mVisible = false;
            _mMenuSpriteSelect[_mIDChoice].mVisible = true;

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Down))
            {
                if (_mIDChoice < 3)
                {
                    _mMenuSprite[_mIDChoice].mVisible = true;
                    _mMenuSpriteSelect[_mIDChoice].mVisible = false;
                    _mIDChoice++;
                }
            }
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Up))
            {
                if (_mIDChoice > 0)
                {
                    _mMenuSprite[_mIDChoice].mVisible = true;
                    _mMenuSpriteSelect[_mIDChoice].mVisible = false;
                    _mIDChoice--;
                }
            }

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                switch(_mIDChoice)
                {
                    case 0: _mGame.ChangeLevel(LevelName.Level_Intro); break;
                    case 1: _mGame.ChangeLevel(LevelName.Level_Tuto); break;
                    case 2: _mGame.ChangeLevel(LevelName.Level_Credits); break;
                    case 3: _mGame.Exit(); break;
                }
            }
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneMenu);
        }
    }
}
