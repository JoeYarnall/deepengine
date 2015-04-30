using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepEngine
{
    public class CameraSystem : System
    {
        public CameraSystem()
            : base(true, false, EngineSystemIds.Camera, Aspect.GetListForAll(typeof(CCameraPlacement), typeof(CViewport)), new Dictionary<int, MessageHandler>())
        {
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public override void Update(GameTime gameTime)
        {
            //Update each cameras transform
            foreach (Entity e in EntityList)
            {
                if (e.HasComponent<CViewport>() && e.HasComponent<CCameraPlacement>())
                {
                    var vpcomp = e.GetComponent<CViewport>();
                    var camcomp = e.GetComponent<CCameraPlacement>();

                    //TODO: Move To A Script
                    //if (camcomp.StayWithinWorldBounds)
                    //{
                    //    if (camcomp.Position.X > GameState.WorldUnitWidth)
                    //        camcomp.Position = new Vector2(GameState.WorldUnitWidth, camcomp.Position.Y);
                    //    if (camcomp.Position.X < 0)
                    //        camcomp.Position = new Vector2(0, camcomp.Position.Y);
                    //    if (camcomp.Position.Y > GameState.WorldUnitHeight)
                    //        camcomp.Position = new Vector2(camcomp.Position.X, GameState.WorldUnitHeight);
                    //    if (camcomp.Position.Y < 0)
                    //        camcomp.Position = new Vector2(camcomp.Position.X, 0);
                    //}

                    camcomp.Transform =
                    Matrix.CreateTranslation(new Vector3(-camcomp.Position.X * vpcomp.WorldUnitPixelWidth, -camcomp.Position.Y * vpcomp.WorldUnitPixelHeight, 0)) *
                    Matrix.CreateRotationZ(camcomp.Rotation) *
                    Matrix.CreateScale(new Vector3(camcomp.Zoom, camcomp.Zoom, 1)) *
                    Matrix.CreateTranslation(new Vector3(vpcomp.Width * 0.5f, vpcomp.Height * 0.5f, 0));
                }

            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Do Nothing
        }

        public override void Destroy()
        {
            base.Destroy();
        }
    }
}
