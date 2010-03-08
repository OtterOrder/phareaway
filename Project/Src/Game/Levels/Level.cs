using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;

namespace PhareAway
{
    public abstract class Level
    {
        protected ContentManager _mContent;
        protected PhareAwayGame  _mGame;

        public Level(PhareAwayGame _Game, ContentManager _Content)
        {
            _mContent = _Content;
            _mGame = _Game;
        }

        public abstract void Init();
        public abstract void Update(float _Dt);
        public abstract void Draw(SpriteBatch _SprBatch, GraphicsDeviceManager _GraphicsManager);
    }
}
