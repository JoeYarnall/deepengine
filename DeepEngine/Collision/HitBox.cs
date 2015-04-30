using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public struct HitBox
    {
        private float x;
        private float y;
        private float width;
        private float height;

        public HitBox(float x, float y, float width, float height)
        {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        public bool Contains(Vector2 point)
        {
            if (point == null)
                return false;

            if (point.X >= x && point.X <= x + width && point.Y >= y && point.Y <= y + height)
                return true;
            else
                return false;
        }
    }
}
