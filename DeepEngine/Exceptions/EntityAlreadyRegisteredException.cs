using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeepEngine
{
    [Serializable()]
    public class EntityAlreadyRegisteredException : Exception
    {
        public EntityAlreadyRegisteredException() : base() { }
        public EntityAlreadyRegisteredException(string message) : base(message) { }
        public EntityAlreadyRegisteredException(string message, Exception inner) : base(message, inner) { }
    }
}

