using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DeepEngine
{
    public class CCameraPlacement : Component
    {
        public bool StayWithinWorldBounds { get; set; }
        public Matrix Transform { get; set; }
        public Vector2 Position { get; set; }
        public float Zoom { get; set; }
        public float Rotation { get; set; }

        public CCameraPlacement()
            : base(true, false)
        {
            Transform = Matrix.Identity;
            Position = Vector2.Zero;
            Zoom = 1f;
            Rotation = 0f;
        }
    }
}
