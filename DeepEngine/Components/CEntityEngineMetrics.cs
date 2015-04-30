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
    public class CEntityEngineMetrics : Component
    {
        public int TotalEntities { get; set; }
        public int ActiveEntities { get; set; }
        public int TotalSystems { get; set; }
        public int ActiveSystems { get; set; }

        public CEntityEngineMetrics()
            : base(true, false)
        {
        }
    }
}
