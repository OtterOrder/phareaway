using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace PhareAway
{
    public class LevelTuto : Level
    {
        private PhareAwayGame _mGame;

        /*private Sprite _mSprTuto;
        private Sprite _mCenter;

        private UInt32 _mSceneTuto;
        private Camera _mDefaultCam;*/

        private VideoPlayer _mPlayer = null;

        public LevelTuto(PhareAwayGame _Game, ContentManager _Content)
        : base(_Game, _Content)
        {
            //_mSceneTuto = SceneManager.Singleton.CreateScene();
            _mGame = _Game;
        }

        public override void Init()
        {
            /*_mDefaultCam = SceneManager.Singleton.GetNewCamera(_mSceneTuto);
            _mDefaultCam.SetViewportParam(0, 0, 1.0f, 1.0f);
            _mDefaultCam.Position = new Vector2(0.0f, 0.0f);

            _mSprTuto = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Tutorial/Tuto_bg", _mContent, _mSceneTuto);
            _mSprTuto.mPosition = new Vector2(0.0f, 0.0f);
            _mSprTuto.mOrigin = new Vector2(0.0f, 0.0f);

            _mCenter = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Tutorial/Phare_neutre", _mContent, _mSceneTuto, 4, 15.0f);
            _mCenter.mPosition = new Vector2(640.0f, 360.0f);
            _mCenter.mOrigin = new Vector2(_mCenter.Width/2.0f, _mCenter.Height/2.0f);
            _mCenter.AnimPlayer.Loop = true;*/

            _mPlayer = new VideoPlayer();
            _mPlayer.Play(_mContent.Load<Video>("Graphics/Videos/Bear"));
            _mPlayer.IsLooped = true;
            _mPlayer.Pause();
        }

        public override void Update(float _Dt)
        {
            if (_mPlayer.State == MediaState.Paused || _mPlayer.State == MediaState.Stopped)
                _mPlayer.Resume();

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                _mPlayer.Stop();
                _mGame.ChangeLevel(LevelName.Level_Menu);
            }
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            //SceneManager.Singleton.DrawScene(_SprBatch, _GraphicsManager, _mSceneTuto);

            _SprBatch.Begin();
            
            _SprBatch.Draw(_mPlayer.GetTexture(),
                            Vector2.Zero, new Rectangle(0, 0, _GraphicsManager.PreferredBackBufferWidth, _GraphicsManager.PreferredBackBufferHeight),
                            Color.White,
                            0,
                            Vector2.Zero,
                            1.0f,
                            SpriteEffects.None,
                            0.1f);
             
            _SprBatch.End();

        }
    }
}
