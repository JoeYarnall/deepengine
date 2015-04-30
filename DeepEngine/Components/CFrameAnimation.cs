using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CFrameAnimation : Component
    {
        public float FrameRate { get; set; }
        public float EllapsedTime { get; set; }
        public bool RandomFrame { get; set; }
        public bool Loop { get; set; }
        public int Direction { get; set; }

        public CFrameAnimation()
            : base(true, false)
        {
        }
    }
}
