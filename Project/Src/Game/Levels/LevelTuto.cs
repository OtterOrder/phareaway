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

        private List<Video> _mVideos = new List<Video>();
        private int         _mCurrentVideo = 0;
        private VideoPlayer _mPlayer = null;

        public LevelTuto(PhareAwayGame _Game, ContentManager _Content)
        : base(_Game, _Content)
        {
            _mGame = _Game;
        }

        public override void Init()
        {
            _mVideos.Add(_mContent.Load<Video>("Graphics/Videos/Bear"));

            _mPlayer = new VideoPlayer();

            _mPlayer.Play(_mVideos[0]);
            _mPlayer.IsLooped = true;
            _mPlayer.Pause();
        }

        public override void Update(float _Dt)
        {
            if (_mPlayer.State == MediaState.Paused || _mPlayer.State == MediaState.Stopped)
                _mPlayer.Resume();

            if (InputManager.Singleton.IsKeyJustPressed(Keys.Enter))
            {
                _mPlayer.Play(_mVideos[0]);
                _mPlayer.Stop();
                _mGame.ChangeLevel(LevelName.Level_Menu);

                return;
            }
            
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Right))
            {
                _mCurrentVideo++;
                if (_mCurrentVideo >= _mVideos.Count)
                    _mCurrentVideo = 0;

                _mPlayer.Play(_mVideos[_mCurrentVideo]);
            }
            if (InputManager.Singleton.IsKeyJustPressed(Keys.Left))
            {
                _mCurrentVideo--;
                if (_mCurrentVideo < 0)
                    _mCurrentVideo = _mVideos.Count - 1;

                _mPlayer.Play(_mVideos[_mCurrentVideo]);
            }
        }

        public override void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            _GraphicsManager.GraphicsDevice.Clear(Color.Black);

            _SprBatch.Begin();
            
            _SprBatch.Draw(_mPlayer.GetTexture(),
                            Vector2.Zero,
                            new Rectangle(0, 0, _mPlayer.Video.Width, _mPlayer.Video.Height),
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
