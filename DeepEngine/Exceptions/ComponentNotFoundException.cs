using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeepEngine
{
    [Serializable()]
    public class ComponentNotFoundException : Exception
    {
        public ComponentNotFoundException() : base() { }
        public ComponentNotFoundException(string message) : base(message) { }
        public ComponentNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}
