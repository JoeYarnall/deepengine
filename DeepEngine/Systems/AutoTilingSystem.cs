using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using C3.XNA;

namespace DeepEngine
{
    public class AutoTilingSystem : System
    {
        public AutoTilingSystem()
            : base(true, false, SystemIds.AutoTiling, Aspect.GetListForAll(typeof(CWorldPlacement), typeof(CAutoTileProperties), typeof(CSprite)), new Dictionary<int, MessageHandler>())
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
                var placComp = e.GetComponent<CWorldPlacement>();
                var tileComp = e.GetComponent<CAutoTileProperties>();
                var spriteComp = e.GetComponent<CSprite>();

                Neighbours neighbours = Neighbours.None;

                MessageData top = new MessageData(new Vector3(placComp.Position.X, placComp.Position.Y - 1, placComp.Layer));
                MessageData right = new MessageData(new Vector3(placComp.Position.X + 1, placComp.Position.Y, placComp.Layer));
                MessageData bottom = new MessageData(new Vector3(placComp.Position.X, placComp.Position.Y + 1, placComp.Layer));
                MessageData left = new MessageData(new Vector3(placComp.Position.X - 1, placComp.Position.Y, placComp.Layer));

                SendMessage(MessageIds.GetInstanceIDAt, ref top, e);
                SendMessage(MessageIds.GetInstanceIDAt, ref right, e);
                SendMessage(MessageIds.GetInstanceIDAt, ref bottom, e);
                SendMessage(MessageIds.GetInstanceIDAt, ref left, e);

                if (top.I1 != EntityEngine.InvalidInstanceID)
                {
                    neighbours |= Neighbours.Top;
                }

                if (right.I1 != EntityEngine.InvalidInstanceID)
                {
                    neighbours |= Neighbours.Right;
                }

                if (bottom.I1 != EntityEngine.InvalidInstanceID)
                {
                    neighbours |= Neighbours.Bottom;
                }

                if (left.I1 != EntityEngine.InvalidInstanceID)
                {
                    neighbours |= Neighbours.Left;
                }

                spriteComp.FrameIndex = tileComp.FrameIndexWithNeighbour[neighbours];
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Do Nothing
        }
    }
}
