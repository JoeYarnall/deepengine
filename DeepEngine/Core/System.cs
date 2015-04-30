using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public delegate void MessageHandler(ref MessageData data, Entity target, object sender);

    public abstract class System : DeepObject
    {
        public int SystemId { get; private set; }

        protected Aspect requiredAspects;
        public Aspect RequiredAspects
        {
            get { return requiredAspects; }
        }

        public Dictionary<int, MessageHandler> SupportedMessages { get; set; }
        public List<Entity> EntityList { get; set; }
        public RequestHandler RequestHandler { get; set; }
        public ReleaseHandler ReleaseHandler { get; set; }
        
        /// <summary>
        /// The System Constructor.
        /// </summary>
        public System(bool active, bool persist, int systemId, Aspect requiredAspects, Dictionary<int, MessageHandler> supportedMessages)
        {
            Active = active;
            Persist = persist;
            this.requiredAspects = requiredAspects;
            EntityList = new List<Entity>();
            SupportedMessages = supportedMessages;
            SystemId = systemId;
        }

        /// <summary>
        /// A method used to perform startup logic, it is called before the first Update call.
        /// </summary>
        public virtual void Initialize()
        {
            EntityList.Clear();
            EntityList = RequestHandler(RequiredAspects);
        }

        public void DeliverMessage(int messageId, ref MessageData data, Entity target, object sender)
        {
            foreach (var kv in SupportedMessages)
            {
                if (kv.Key == messageId)
                {
                    kv.Value(ref data, target, sender);
                }

            }
        }

        /// <summary>
        /// Main Update Loop.
        /// </summary>
        /// <param name="gameTime">Delta Time.</param>
        public abstract void Update(GameTime gameTime);

        /// <summary>
        /// Main Draw Loop.
        /// </summary>
        /// <param name="gameTime">Delta Time.</param>
        public abstract void Draw(GameTime gameTime, SpriteBatch sb);

        /// <summary>
        /// Tells the EntityEngine that it is finished with the NodeLists it is using.
        /// </summary>
        public virtual void Destroy()
        {
            ReleaseHandler(RequiredAspects);
            EntityList.Clear();
        }
    }
}
