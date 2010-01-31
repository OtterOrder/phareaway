using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Linq;
using System.Text;

namespace PhareAway
{
    public class Player : GameComponent
    {
        private Sprite          _mSpr = null;
        private ContentManager  _mContent = null;

        KeyboardState CurKeyboardState;
        KeyboardState LastKeyboardState;

        public Player(PhareAwayGame _Game) : base(_Game)
        {
            _mContent = _Game.Content;

            _mSpr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk", _mContent, 4, 15);
            _mSpr.Depth = 0.06f;
            _mSpr._mPosition.X = 405.42f;
            _mSpr._mPosition.Y = 105.42f;
            _mSpr.SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(_mSpr.Width, _mSpr.Height));

            if (_mSpr.AnimPlayer != null)
            {
                _mSpr.AnimPlayer.Loop = true;
                _mSpr.AnimPlayer.Speed = 1.0f;
            }

            _Game.Components.Add(this);
        }

         public override void Update(GameTime gameTime)
         {
             ////. Test
             CurKeyboardState = Keyboard.GetState();

             bool Play = false;

             float Speed = 0.1f * (float)gameTime.ElapsedGameTime.TotalMilliseconds; ;

             if (CurKeyboardState.IsKeyDown(Keys.Right) /*&& LastKeyboardState.IsKeyUp(Keys.Right)*/)
             {
                 _mSpr._mPosition.X += Speed;

                 BoundingBox Collision = CollisionsManager.Singleton.Collide(_mSpr, 1);
                 if (Collision != null)
                     _mSpr._mPosition.X -= Speed;
                 else
                     Play = true;
             }
             else
                 if (CurKeyboardState.IsKeyDown(Keys.Left) /*&& LastKeyboardState.IsKeyUp(Keys.Left)*/)
                 {
                     _mSpr._mPosition.X -= Speed;

                     BoundingBox Collision = CollisionsManager.Singleton.Collide(_mSpr, 1);
                     if (Collision != null)
                         _mSpr._mPosition.X += Speed;
                     else
                         Play = true;
                 }


             if (CurKeyboardState.IsKeyDown(Keys.Up))
             {
                 _mSpr._mPosition.Y -= Speed;

                 BoundingBox Collision = CollisionsManager.Singleton.Collide(_mSpr, 1);
                 if (Collision != null)
                     _mSpr._mPosition.Y += Speed;
                 else
                     Play = true;
             }
             else
                 if (CurKeyboardState.IsKeyDown(Keys.Down))
                 {
                     _mSpr._mPosition.Y += Speed;

                     BoundingBox Collision = CollisionsManager.Singleton.Collide(_mSpr, 1);
                     if (Collision != null)
                         _mSpr._mPosition.Y -= Speed;
                     else
                         Play = true;
                 }

             if (_mSpr.AnimPlayer != null)
             {
                 _mSpr.AnimPlayer.Play = Play;

                 if (!Play)
                     _mSpr.AnimPlayer.CurrentFrame = 0;
             }

             LastKeyboardState = CurKeyboardState;

         }
    }
}
