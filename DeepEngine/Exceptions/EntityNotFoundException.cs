﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace DeepEngine
{
    [Serializable()]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException() : base() { }
        public EntityNotFoundException(string message) : base(message) { }
        public EntityNotFoundException(string message, Exception inner) : base(message, inner) { }
    }
}