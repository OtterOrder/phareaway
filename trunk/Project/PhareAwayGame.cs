using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Storage;
using Microsoft.Xna.Framework.Media;

namespace PhareAway
{
    public class PhareAwayGame : Microsoft.Xna.Framework.Game
    {

        private GraphicsDeviceManager graphics;

        private const int BackBufferWidth = 1024;
        private const int BackBufferHeight = 768;

        public PhareAwayGame()
        {
            graphics = new GraphicsDeviceManager(this);
            graphics.PreferredBackBufferWidth = BackBufferWidth;
            graphics.PreferredBackBufferHeight = BackBufferHeight;
        }
    }
}
