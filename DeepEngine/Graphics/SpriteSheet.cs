using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class SpriteSheet
    {
        public Texture2D Texture { get; set; }
        public int XCount { get; private set; }
        public int YCount { get; private set; }
        private int xFrame;
        private int yFrame;
        public int Padding { get; private set; }

        public SpriteSheet(Texture2D texture, int xCount, int yCount, int padding)
        {
            this.Padding = padding;
            this.XCount = xCount;
            this.YCount = yCount;
            this.Texture = texture;
            this.xFrame = texture.Width / xCount;
            this.yFrame = texture.Height / yCount;
        }

        public Rectangle GetBounds(int x, int y)
        {
            return new Rectangle(x * xFrame + Padding, y * yFrame + Padding, xFrame - 2 * Padding, yFrame - 2 * Padding);
        }
    }
}
