﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhareAway
{
    class Scene
    {
        private List<Sprite>        _mSprList = new List<Sprite>();
        private List<Background>    _mBgList  = new List<Background>();

        private UInt32 _mId;

        //-------------------------------------------------------------------------
        public Scene(UInt32 id)
        {
            _mId = id;
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
                if (ItSpr.Current.mVisible)
                    ItSpr.Current.Draw(_SprBatch);
            }

            _SprBatch.End();

        }

        public void AddSprite(Sprite _Spr)
        {
            _mSprList.Add(_Spr);
        }

        public void AddBackground(Background _Bg)
        {
            _mBgList.Add(_Bg);
        }

        public void SortSprites()
        {
            _mSprList.Sort(new SpriteComparer());
        }

        public void SortBackgrounds()
        {
            _mBgList.Sort(new BackgroundComparer());
        }

    }
}