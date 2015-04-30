using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public class WorldSpriteRenderSystem : System
    {
        private Entity Camera { get; set; }

        public WorldSpriteRenderSystem()
            : base(true, false, EngineSystemIds.WorldSpriteRender, Aspect.GetListForAll(typeof(CSprite), typeof(CWorldPlacement)), new Dictionary<int, MessageHandler>())
        {
            SupportedMessages.Add(EngineMessageIds.EntityCreated, OnEntitySpawnedMessage);
        }

        public void OnEntitySpawnedMessage(ref MessageData data, Entity target, object sender)
        {
            if (Camera == null && target.HasComponent<CCameraPlacement>())
                Camera = target;
        }

        public override void Initialize()
        {
            base.Initialize();

            Camera = null;
        }

        public override void Update(GameTime gameTime)
        {

        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            if (Camera != null)
            {
                sb.Begin(SpriteSortMode.BackToFront, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, null, null, Camera.GetComponent<CCameraPlacement>().Transform);

                //Construct TexAndPos for each Entity that has a Sprite and a Placement
                foreach (Entity e in EntityList.Where(e => e.GetComponent<CWorldPlacement>().Visible))
                {
                    var spriteComp = e.GetComponent<CSprite>();
                    var posComp = e.GetComponent<CWorldPlacement>();
                        
                    Rectangle sourceRect = spriteComp.Sprite.GetBounds(spriteComp.VarietyIndex, spriteComp.FrameIndex);
                    Rectangle destRect = new Rectangle((int)(posComp.Position.X * Camera.GetComponent<CViewport>().WorldUnitPixelWidth), (int)(posComp.Position.Y * Camera.GetComponent<CViewport>().WorldUnitPixelHeight), (int)(posComp.Size.X * Camera.GetComponent<CViewport>().WorldUnitPixelWidth), (int)(posComp.Size.Y * Camera.GetComponent<CViewport>().WorldUnitPixelHeight));
                    Vector2 origin = new Vector2(sourceRect.Width * 0.5f, sourceRect.Height * 0.5f);

                    sb.Draw(spriteComp.Sprite.Texture, destRect, sourceRect, spriteComp.Tint * spriteComp.Alpha, posComp.Rotation, origin, SpriteEffects.None, posComp.Position.Z);
                }

                sb.End();
            }
        }

        public override void Destroy()
        {
            Camera = null;

            base.Destroy();
        }
    }
}