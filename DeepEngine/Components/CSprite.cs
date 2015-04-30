using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CSprite : Component
    {
        public Sprite Sprite { get; set; }
        public int FrameIndex { get; set; }
        public int VarietyIndex { get; set; }
        public Color Tint { get; set; }
        public float Alpha { get; set; }

        public CSprite()
            : base(true, false)
        {
        }
    }
}
