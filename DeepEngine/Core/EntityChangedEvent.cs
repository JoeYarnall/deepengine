using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class EntityChangedEvent : EventArgs
    {
        private Entity entity;
        public Entity Entity
        {
            get { return entity; }
            set { entity = value; }
        }

        private Type target;
        public Type Target
        {
            get { return target; }
        }

        private ACTION action;
        public ACTION Action
        {
            get { return action; }
        }

        public EntityChangedEvent(Entity entity, Type target, ACTION action)
        {
            this.entity = entity;
            this.target = target;
            this.action = action;
        }
    }

    public enum ACTION
    {
        ADD,
        REMOVE
    }
}
