using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class ScriptSystem : System
    {
        public ScriptSystem()
            : base(true, false, EngineSystemIds.Script, Aspect.GetListForAll(typeof(CScriptHandlers)), new Dictionary<int, MessageHandler>())
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity e in EntityList)
            {
                var scriptHandlers = e.GetComponent<CScriptHandlers>();

                foreach (int callbackID in scriptHandlers.ScriptHandlerIDs)
                {
                    GameRegistry.FetchScriptCallback(callbackID)(e, gameTime);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Do Nothing
        }
    }
}
