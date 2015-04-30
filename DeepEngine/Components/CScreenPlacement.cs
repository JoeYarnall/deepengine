using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CScreenPlacement : Component
    {
        public bool Visible { get; set; }
        public Rectangle Bounds { get; set; }
        public int Depth { get; set; }
        public float Rotation { get; set; }

        public CScreenPlacement()
            : base(true, false)
        {
        }
    }
}
