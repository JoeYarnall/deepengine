using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CCollider : Component
    {
        public bool Collidable { get; set; }
        public bool Pickable { get; set; }
        public HitBox HitBox { get; set; }
        public Texture2D PixelMask { get; set; }

        public CCollider()
            : base(true, false)
        {
        }
    }
}
