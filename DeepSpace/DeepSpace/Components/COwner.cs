using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;

namespace DeepSpace
{
    public class COwner : Component
    {
        public int PlayerNumber { get; set; }

        public COwner()
            : base(true, false)
        {
            PlayerNumber = 0;
        }
    }
}
