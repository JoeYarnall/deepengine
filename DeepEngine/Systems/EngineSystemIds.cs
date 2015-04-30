using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public static class EngineSystemIds
    {
        public const int WorldSpriteRender      = 0x00000001;
        public const int WorldTextRender        = 0x00000002;
        public const int ScreenSpriteRender     = 0x00000003;
        public const int ScreenTextRender       = 0x00000004;
        public const int Script                 = 0x00000005;
        public const int Input                  = 0x00000006;
        public const int Camera                 = 0x00000007;
        public const int Collision              = 0x00000008;
        public const int ParallaxBackground     = 0x00000009;
        public const int ViewPort               = 0x00000010;
    }
}