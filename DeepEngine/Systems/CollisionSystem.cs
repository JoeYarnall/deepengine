using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CollisionSystem : System
    {
        private Entity Camera;

        public CollisionSystem()
            : base(true, false, EngineSystemIds.Collision, Aspect.GetListForAll(typeof(CCollider)), new Dictionary<int, MessageHandler>())
        {
            SupportedMessages.Add(EngineMessageIds.EntityCreated, OnEntitySpawnedMessage);
            SupportedMessages.Add(EngineMessageIds.RaycastWorld, OnRaycastWorldMessage);
        }

        public void OnEntitySpawnedMessage(ref MessageData data, Entity target, object sender)
        {
            if (target.HasComponent<CWorldPlacement>() && target.HasComponent<CCollider>())
            {
                target.GetComponent<CCollider>().HitBox = new HitBox(
                    target.GetComponent<CWorldPlacement>().Position.X, 
                    target.GetComponent<CWorldPlacement>().Position.Y, 
                    target.GetComponent<CWorldPlacement>().Size.X, 
                    target.GetComponent<CWorldPlacement>().Size.Y);
            }

            if (target.HasComponent<CCameraPlacement>() && target.HasComponent<CViewport>())
            {
                Camera = target;
            }
        }

        public void OnRaycastWorldMessage(ref MessageData data, Entity target, object sender)
        {
            if (Camera != null)
            {
                float x;
                float y;

                if(data.P1.X >= 0.5 * Camera.GetComponent<CViewport>().Width)
                    x = Camera.GetComponent<CCameraPlacement>().Position.X + (data.P1.X - (0.5f * Camera.GetComponent<CViewport>().Width)) / Camera.GetComponent<CViewport>().WorldUnitPixelWidth;
                else
                    x = Camera.GetComponent<CCameraPlacement>().Position.X - ((0.5f * Camera.GetComponent<CViewport>().Width) - data.P1.X) / Camera.GetComponent<CViewport>().WorldUnitPixelWidth;

                if (data.P1.Y >= 0.5 * Camera.GetComponent<CViewport>().Height)
                    y = Camera.GetComponent<CCameraPlacement>().Position.Y + (data.P1.Y - (0.5f * Camera.GetComponent<CViewport>().Height)) / Camera.GetComponent<CViewport>().WorldUnitPixelHeight;
                else
                    y = Camera.GetComponent<CCameraPlacement>().Position.Y - ((0.5f * Camera.GetComponent<CViewport>().Height) - data.P1.Y) / Camera.GetComponent<CViewport>().WorldUnitPixelHeight;

                Vector2 rayInWorldSpace = new Vector2(x, y);

                foreach (Entity e in EntityList.Where(e => e.GetComponent<CCollider>().Pickable))
                {
                    if (e.GetComponent<CCollider>().HitBox.Contains(rayInWorldSpace))
                    {
                        data.SetInt3Response(e.InstanceID);
                        data.Handled = true;
                    }
                }
            }
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            foreach (Entity e in EntityList)
            {
                if (e.HasComponent<CWorldPlacement>() && e.HasComponent<CCollider>())
                {
                    e.GetComponent<CCollider>().HitBox = new HitBox(
                        e.GetComponent<CWorldPlacement>().Position.X - (0.5f * e.GetComponent<CWorldPlacement>().Size.X),
                        e.GetComponent<CWorldPlacement>().Position.Y - (0.5f * e.GetComponent<CWorldPlacement>().Size.Y),
                        e.GetComponent<CWorldPlacement>().Size.X,
                        e.GetComponent<CWorldPlacement>().Size.Y);
                }
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //TODO
        }
    }
}