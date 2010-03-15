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

        private List<Scene>         _mSceneList = new List<Scene>();

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
        public UInt32 CreateScene()
        {
            UInt32 id = (UInt32)_mSceneList.Count;
            Scene scene = new Scene(id);
            _mSceneList.Add(scene);

            return id;
        }

        public Sprite GetNewSprite(string _FileName, ContentManager _ContentManager, UInt32 _SceneId)
        {
            Sprite spr = new Sprite(_FileName, _ContentManager);
            _mSceneList[(int)_SceneId].AddSprite(spr);

            return spr;
        }

        public Sprite GetNewSprite(string _FileName, ContentManager _ContentManager, UInt32 _SceneId, UInt32 _NbFrames, float _Fps)
        {
            Sprite spr = new Sprite(_FileName, _ContentManager, _NbFrames, _Fps);
            _mSceneList[(int)_SceneId].AddSprite(spr);

            return spr;
        }

        public Background GetNewBackground(string _FileName, ContentManager _ContentManager, UInt32 _SceneId)
        {
            Background bg = new Background(_FileName, _ContentManager);
            _mSceneList[(int)_SceneId].AddBackground(bg);

            return bg;
        }

        public Camera GetNewCamera(UInt32 _SceneId)
        {
            Camera camera = new Camera();
            _mSceneList[(int)_SceneId].AddCamera(camera);

            return camera;
        }

        public Scene GetScene(UInt32 _SceneId)
        {
            return _mSceneList[(int)_SceneId];
        }

        public void RemoveSprite (Sprite _Sprite, UInt32 _SceneId)
        {
            if (_Sprite != null)
                _mSceneList[(int)_SceneId].RemoveSprite(_Sprite);
        }

        //-------------------------------------------------------------------------
        public void SortSprites()
        {
            foreach (Scene scene in _mSceneList)
            {
                scene.SortSprites();
            }
        }

        public void SortBackgrounds()
        {
            foreach (Scene scene in _mSceneList)
            {
                scene.SortBackgrounds();
            }
        }

        //-------------------------------------------------------------------------
        public void Update(float _Dt)
        {
            foreach (Scene scene in _mSceneList)
            {
                scene.Update(_Dt);
            }
        }

        //-------------------------------------------------------------------------
        public void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager)
        {

            foreach (Scene scene in _mSceneList)
            {
                scene.Draw(_SprBatch, _GraphicsManager);
            }  
        }

        public void DrawScene(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager, UInt32 _SceneId)
        {
            _mSceneList[(int)_SceneId].Draw(_SprBatch, _GraphicsManager);
        }
    }
}