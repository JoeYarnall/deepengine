using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class ScreenTextRenderSystem : System
    {
        private CViewport ViewPort { get; set; }

        public ScreenTextRenderSystem()
            : base(true, true, EngineSystemIds.ScreenTextRender, Aspect.GetListForAll(typeof(CText)), new Dictionary<int, MessageHandler>())
        {
            SupportedMessages.Add(EngineMessageIds.EntityCreated, OnEntitySpawnedMessage);
        }

        public void OnEntitySpawnedMessage(ref MessageData data, Entity target, object sender)
        {
            if (ViewPort == null && target.HasComponent<CViewport>())
                ViewPort = target.GetComponent<CViewport>();
        }

        public override void Initialize()
        {
            base.Initialize();
            ViewPort = null;
        }

        public override void Update(GameTime gameTime)
        {
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (ViewPort != null)
            {
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, null, null);

                foreach (Entity e in EntityList.Where(e => e.Active))
                {
                    if (e.HasComponent<CText>())
                    {
                        var renderComp = e.GetComponent<CText>();
                        //sb.DrawDebugOverlay(new Rectangle(0, 0, ViewPort.Width, (int)renderComp.Font.MeasureString(renderComp.Text).Y));
                        sb.DrawString(renderComp.Font, renderComp.Text, new Vector2(10, 10), renderComp.TextColor * renderComp.TextAlpha, 0f, Vector2.Zero, 1f, SpriteEffects.None, 1f);
                    }
                }

                sb.End();
            }
        }
    }
}