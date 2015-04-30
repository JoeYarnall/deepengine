using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class ViewPortSystem : System
    {
        public ViewPortSystem()
            : base(true, false, EngineSystemIds.ViewPort, Aspect.GetListForAll(typeof(CViewport)), new Dictionary<int, MessageHandler>())
        {
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity e in EntityList)
            {

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            
        }
    }
}
