using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CChildren : Component
    {
        public List<Entity> Children { get; set; }

        public CChildren()
            : base(true, false)
        {
            Children = new List<Entity>();
        }
    }
}
