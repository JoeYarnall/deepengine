using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CAutoTileProperties : Component
    {
        public Dictionary<Neighbours, int> FrameIndexWithNeighbour { get; set; }

        public CAutoTileProperties()
            : base(true, false)
        {
        }
    }

    [Flags] 
    public enum Neighbours 
    {
        None = 0,
        Left = 1, 
        Bottom = 2, 
        Right = 4, 
        Top = 8 
    }
}
