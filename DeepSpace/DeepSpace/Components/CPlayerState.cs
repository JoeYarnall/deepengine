using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;

namespace DeepSpace
{
    public class CPlayerState : Component
    {
        public Entity PickedEntity { get; set; }
        public int PlayerNumber { get; set; }

        public CPlayerState()
            :base(true, false)
        {
        }
    }

}
