﻿using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace PhareAway
{
    public class Level
    {
        private Background Bg;
        private Sprite Spr, Spr2;

        KeyboardState CurKeyboardState;
        KeyboardState LastKeyboardState;

        // Level content.        
        public ContentManager Content
        {
            get { return content; }
        }
        ContentManager content;

        public Level(IServiceProvider serviceProvider)
        {
            content = new ContentManager(serviceProvider, "Resources");

            Bg = SceneManager.Singleton.GetNewBackground("Graphics/Backgrounds/bg_Space_01", Content);
            Bg.Depth = 0.5f;
            Bg._mSpeed.Y = -0.05f;

            Spr = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk", Content, 4, 15);
            Spr.Depth = 0.06f;
            Spr._mPosition.X = 405.42f;
            Spr._mPosition.Y = 105.42f;
            Spr.SetBoundingBox(0, new Vector2(0.0f, 0.0f), new Vector2(Spr.Width, Spr.Height));
            if (Spr.AnimPlayer != null)
            {
                Spr.AnimPlayer.Loop = true;
                Spr.AnimPlayer.Speed = 1.0f;
            }

            Spr2 = SceneManager.Singleton.GetNewSprite("Graphics/Sprites/Inside/Characters/Archi/Archi_Walk", Content);
            Spr2.Depth = 0.07f;
            Spr2._mPosition.X = 100.0f;
            Spr2._mPosition.Y = 100.0f;
            Spr2.SetBoundingBox(1, new Vector2(0.0f, 0.0f), new Vector2(Spr2.Width, Spr2.Height));
        }

        public void Update(float _Dt)
        {
            ////. Test
            CurKeyboardState = Keyboard.GetState();

            bool Play = false;

            float Speed = 0.1f*_Dt;

            if (CurKeyboardState.IsKeyDown(Keys.Right) /*&& LastKeyboardState.IsKeyUp(Keys.Right)*/)
            {
                Spr._mPosition.X += Speed;

                BoundingBox Collision = CollisionsManager.Singleton.Collide(Spr, 1);
                if (Collision != null)
                    Spr._mPosition.X -= Speed;
                else
                    Play = true;
            }
            else
            if (CurKeyboardState.IsKeyDown(Keys.Left) /*&& LastKeyboardState.IsKeyUp(Keys.Left)*/)
            {
                Spr._mPosition.X -= Speed;

                BoundingBox Collision = CollisionsManager.Singleton.Collide(Spr, 1);
                if (Collision != null)
                    Spr._mPosition.X += Speed;
                else
                    Play = true;
            }


            if (CurKeyboardState.IsKeyDown(Keys.Up))
            {
                Spr._mPosition.Y -= Speed;

                BoundingBox Collision = CollisionsManager.Singleton.Collide(Spr, 1);
                if (Collision != null)
                    Spr._mPosition.Y += Speed;
                else
                    Play = true;
            }
            else
            if (CurKeyboardState.IsKeyDown(Keys.Down))
            {
                Spr._mPosition.Y += Speed;

                BoundingBox Collision = CollisionsManager.Singleton.Collide(Spr, 1);
                if (Collision != null)
                    Spr._mPosition.Y -= Speed;
                else
                    Play = true;
            }

            if (Spr.AnimPlayer != null)
            {
                Spr.AnimPlayer.Play = Play;

                if (!Play)
                    Spr.AnimPlayer.CurrentFrame = 0;
            }

            LastKeyboardState = CurKeyboardState;
            ////.
        }
    }
}
