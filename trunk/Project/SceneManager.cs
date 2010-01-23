using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhareAway
{
    public class SceneManager
    {
        private static SceneManager _mSingleton = null;

        private List<Sprite>        _mSprList = new List<Sprite>();
        private List<Background>    _mBgList  = new List<Background>();

        //-------------------------------------------------------------------------
        private SceneManager ()
        {
        }

        //-------------------------------------------------------------------------
        public static SceneManager Singleton
        {
            get
            {
                if(_mSingleton == null)
                    _mSingleton = new SceneManager ();

                return _mSingleton;
            }
        }

        //-------------------------------------------------------------------------
        public Sprite GetNewSprite(string _FileName, ContentManager _ContentManager)
        {
            Sprite spr = new Sprite(_FileName, _ContentManager);
            _mSprList.Add(spr);

            _mSprList.Sort(new SpriteComparer());

            return spr;
        }

        public Sprite GetNewSprite(string _FileName, ContentManager _ContentManager, UInt32 _NbFrames, float _Fps)
        {
            Sprite spr = new Sprite(_FileName, _ContentManager, _NbFrames, _Fps);
            _mSprList.Add(spr);

            _mSprList.Sort(new SpriteComparer());

            return spr;
        }

        public Background GetNewBackground(string _FileName, ContentManager _ContentManager)
        {
            Background bg = new Background(_FileName, _ContentManager);
            _mBgList.Add(bg);

            _mBgList.Sort(new BackgroundComparer());

            return bg;
        }

        //-------------------------------------------------------------------------
        public void SortSprites()
        {
            _mSprList.Sort(new SpriteComparer());
        }

        public void SortBackgrounds()
        {
            _mBgList.Sort(new BackgroundComparer());
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            IEnumerator<Background> ItBg = _mBgList.GetEnumerator();
            ItBg.Reset();
            while (ItBg.MoveNext())
            {
                ItBg.Current.Update(_Dt);
            }

            IEnumerator<Sprite> ItSpr = _mSprList.GetEnumerator();
            ItSpr.Reset();
            while (ItSpr.MoveNext())
            {
                ItSpr.Current.Update(_Dt);
            }
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {
            _SprBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            _GraphicsManager.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Wrap;
            _GraphicsManager.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Wrap;

            _GraphicsManager.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Linear;
            _GraphicsManager.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Linear;

            IEnumerator<Background> ItBg = _mBgList.GetEnumerator();
            ItBg.Reset();
            while (ItBg.MoveNext())
            {
                ItBg.Current.Draw(_SprBatch);
            }

            _SprBatch.End();

            //-----------------------
            _SprBatch.Begin(SpriteBlendMode.AlphaBlend, SpriteSortMode.Immediate, SaveStateMode.None);

            _GraphicsManager.GraphicsDevice.SamplerStates[0].AddressU = TextureAddressMode.Clamp;
            _GraphicsManager.GraphicsDevice.SamplerStates[0].AddressV = TextureAddressMode.Clamp;

            _GraphicsManager.GraphicsDevice.SamplerStates[0].MinFilter = TextureFilter.Point;
            _GraphicsManager.GraphicsDevice.SamplerStates[0].MagFilter = TextureFilter.Point;

            IEnumerator<Sprite> ItSpr = _mSprList.GetEnumerator();
            ItSpr.Reset();
            while (ItSpr.MoveNext())
            {
                ItSpr.Current.Draw(_SprBatch);
            }

            _SprBatch.End();
        }
    }
}