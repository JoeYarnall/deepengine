using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CParent : Component
    {
        public Entity Parent { get; set; }

        public CParent()
            : base(true, false)
        {
        }
    }
}
