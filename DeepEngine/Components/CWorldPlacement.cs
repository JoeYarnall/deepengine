using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DeepEngine
{
    public class CWorldPlacement : Component
    {
        public bool Visible { get; set; }
        public int Layer { get; set; }
        public Vector3 Position { get; set; }
        public float Rotation { get; set; }
        public Vector2 Size { get; set; }

        public CWorldPlacement()
            : base(true, false)
        {
        }
    }
}
