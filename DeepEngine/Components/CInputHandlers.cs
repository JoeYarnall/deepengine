using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class CInputHandlers : Component
    {
        public List<int> InputHandlerIDs { get; set; }

        public CInputHandlers()
            : base(true, false)
        {
            InputHandlerIDs = new List<int>();
        }

        public void Add(int handlerID)
        {
            InputHandlerIDs.Add(handlerID);
        }
    }
}
