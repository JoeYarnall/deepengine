using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;

namespace DeepEngine
{
    public abstract class Component : DeepObject
    {
        public Component(bool active, bool persist)
        {
            Active = active;
            Persist = persist;
        }
    }
}
