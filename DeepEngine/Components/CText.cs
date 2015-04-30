using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CText : Component
    {
        public SpriteFont Font { get; set; }
        public Color TextColor { get; set; }
        public float TextAlpha { get; set; }
        public string Text { get; set; }

        public CText()
            : base(true, false)
        {
            Text = "";
        }
    }
}
