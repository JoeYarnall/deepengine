using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CScriptHandlers : Component
    {
        public List<int> ScriptHandlerIDs { get; set; }

        public CScriptHandlers()
            : base(true, false)
        {
            ScriptHandlerIDs = new List<int>();
        }

        public void Add(int handlerID)
        {
            ScriptHandlerIDs.Add(handlerID);
        }
    }
}
