using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DeepEngine
{
    public abstract class DeepObject
    {
        public DeepObject()
        {
            //Many deeps.
        }

        public bool Active { get; set; }
        public bool Persist { get; set; }

        /// <summary>
        /// Sends a message to all other systems.
        /// </summary>
        /// <param name="messageId">The unique identifier of the message, used to determine intent.</param>
        /// <param name="data">The data to be sent, format depends on message type.</param>
        /// <param name="target">the entity that </param>
        public void SendMessage(int messageId, ref MessageData data, Entity target)
        {
            EntityEngine.SendMessage(messageId, ref data, target, this);
        }
    }
}
