using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class ScreenSpriteRenderSystem : System
    {
        public ScreenSpriteRenderSystem()
            : base(true, true, EngineSystemIds.ScreenSpriteRender, Aspect.GetListForAll(typeof(CSprite), typeof(CScreenPlacement)), new Dictionary<int, MessageHandler>())
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, null, null);

            //Construct TexAndPos for each Entity that has a Sprite and a Placement
            foreach (Entity e in EntityList.Where(e => e.GetComponent<CScreenPlacement>().Visible))
            {
                var spriteComp = e.GetComponent<CSprite>();
                var posComp = e.GetComponent<CScreenPlacement>();
                
                Rectangle sourceRect = spriteComp.Sprite.GetBounds(spriteComp.VarietyIndex, spriteComp.FrameIndex);
                Rectangle destRect = posComp.Bounds;
                Vector2 origin = new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f);

                sb.Draw(spriteComp.Sprite.Texture, destRect, sourceRect, spriteComp.Tint * spriteComp.Alpha, posComp.Rotation, origin, SpriteEffects.None, posComp.Depth);
            }

            sb.End();
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}