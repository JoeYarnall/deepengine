using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeepEngine
{
    [Serializable()]
    public class CallbackAlreadyRegisteredException : Exception
    {
        public CallbackAlreadyRegisteredException() : base() { }
        public CallbackAlreadyRegisteredException(string message) : base(message) { }
        public CallbackAlreadyRegisteredException(string message, Exception inner) : base(message, inner) { }
    }
}


