using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeepEngine
{
    [Serializable()]
    public class CallbackNotFoundException : Exception
    {
        public CallbackNotFoundException() : base() { }
        public CallbackNotFoundException(string message) : base(message) { }
        public CallbackNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}

