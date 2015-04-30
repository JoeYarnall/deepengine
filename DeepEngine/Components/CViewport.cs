using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CViewport : Component
    {
        public bool VSync { get; set; }
        public bool AntiAliasing { get; set; }
        public int Width { get; set; }
        public int Height {get; set; }
        public int WorldUnitPixelWidth { get; set; }
        public int WorldUnitPixelHeight { get; set; }
        public bool IsMouseVisible {get; set; }
        public bool IsFullscreen { get; set; }

        public CViewport()
            : base(true, false)
        {
        }
    }
}
