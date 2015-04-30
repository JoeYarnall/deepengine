using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CFrameRate : Component
    {
        public int FrameRate { get; set; }
        public int FrameCounter { get; set; }
        public TimeSpan ElapsedTime { get; set; }

        public CFrameRate()
            : base(true, false)
        {
        }
    }
}
