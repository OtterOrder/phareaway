using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace PhareAway
{
    public class Camera
    {
        private Viewport _mViewport;

        private Vector2 _mPosition;
        private Vector2 _mScreenCenter;

        public Vector2  mFocus { get; set; }
        public Matrix   mTransform { get; set; }
        public float    mMoveSpeed { get; set; }

        public Camera()
        {
            _mViewport.Width = 800;
            _mViewport.Height = 600;
            _mViewport.X = 0;
            _mViewport.Y = 0;
            _mScreenCenter = new Vector2(_mViewport.Width / 2, _mViewport.Height / 2);

            mMoveSpeed = 0.01f;
        }

        public Vector2 Position
        {
            get { return _mPosition; }
            set { _mPosition = value; mFocus = _mPosition; }
        }

        public void SetViewportParam(int _PosX, int _PosY, int _Width, int _Height)
        {
            _mViewport.Width = _Width;
            _mViewport.Height = _Height;
            _mViewport.X = _PosX;
            _mViewport.Y = _PosY;
            _mScreenCenter = new Vector2(_mViewport.Width / 2, _mViewport.Height / 2);
        }

        public void Update(float _Dt)
        {
            mTransform = Matrix.Identity *
                          Matrix.CreateTranslation(-_mPosition.X, -_mPosition.Y, 0) *
                          Matrix.CreateTranslation(_mScreenCenter.X, _mScreenCenter.Y, 0);


            _mPosition.X += (int)((mFocus.X - _mPosition.X) * mMoveSpeed * _Dt);
            _mPosition.Y += (int)((mFocus.Y - _mPosition.Y) * mMoveSpeed * _Dt);
        }

        public void SetCamera(GraphicsDeviceManager _GraphicsManager)
        {
            _GraphicsManager.GraphicsDevice.Viewport = _mViewport;
        }
    }
}