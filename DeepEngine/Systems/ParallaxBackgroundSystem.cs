using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class ParallaxBackgroundSystem : System
    {
        public ParallaxBackgroundSystem()
            : base(true, false, EngineSystemIds.ParallaxBackground, new Aspect(), new Dictionary<int, MessageHandler>())
        {

        }

        public override void Update(GameTime gameTime)
        {
            //Do Nothing
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Rectangle destRect = new Rectangle(0, 0, 1920, 1080);
            //sb.Begin(SpriteSortMode.FrontToBack, BlendState.Opaque, SamplerState.LinearWrap, DepthStencilState.Default, RasterizerState.CullNone);
            //sb.Draw(backgroundTexture, Vector2.Zero, destRect, Color.White, 0, Vector2.Zero, 1, SpriteEffects.None, 0);
            //sb.End();
        }
    }
}
