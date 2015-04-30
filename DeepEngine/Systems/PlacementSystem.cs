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
    public class PlacementSystem : System
    {
        private CGameState GameState { get; set; }
        private EntityGrid Grid { get; set; }

        public PlacementSystem()
            : base(true, false, SystemIds.Placement,Aspect.GetListForOne(typeof(CWorldPlacement)), new Dictionary<int, MessageHandler>())
        {
            SupportedMessages.Add(MessageIds.EntityMoved, OnEntityMovedMessage);
            SupportedMessages.Add(MessageIds.EntitySpawned, OnEntitySpawnedMessage);
            SupportedMessages.Add(MessageIds.GetInstanceIDAt, OnGetEntityAtMessage);
        }

        public override void Initialize()
        {
            base.Initialize();

            Grid = new EntityGrid();
            GameState = null;
        }

        public void OnEntityMovedMessage(ref MessageData data, Entity target, object sender)
        {
            if (target.HasComponent<CWorldPlacement>())
            {
                var pComp = target.GetComponent<CWorldPlacement>();
                SetInstanceIDAt((int)pComp.Position.X, (int)pComp.Position.Y, pComp.Layer, target.InstanceID);
            }
        }

        public void OnEntitySpawnedMessage(ref MessageData data, Entity target, object sender)
        {
            if (GameState == null && target.HasComponent<CGameState>())
            {
                GameState = target.GetComponent<CGameState>();
                Grid.SetSize(GameState.WorldWidth, GameState.WorldHeight, GameState.WorldLayers);
            }

            if (target.HasComponent<CWorldPlacement>())
            {
                var pComp = target.GetComponent<CWorldPlacement>();
                SetInstanceIDAt((int)pComp.Position.X, (int)pComp.Position.Y, pComp.Layer, target.InstanceID);
            }
        }

        public void OnGetEntityAtMessage(ref MessageData data, Entity target, object sender)
        {
            if (!data.Handled) //If the message has already been handled don't respond
            {
                data.SetInt1Response(GetInstanceIDAt(data.V1));
                data.Handled = true;
            }
        }

        private int GetInstanceIDAt(Vector3 loc)
        {
            return Grid.QueryInstanceIDAt((int)loc.X, (int)loc.Y, (int)loc.Z);
        }

        private int GetInstanceIDAt(int x, int y, int layer)
        {
            return Grid.QueryInstanceIDAt(x, y, layer);
        }

        private void SetInstanceIDAt(int x, int y, int layer, int instanceID)
        {
            Grid.Assign(instanceID, x, y, layer);
        }

        private bool QueryInstanceIDAt(int x, int y, int layer)
        {
            if (Grid.QueryInstanceIDAt(x, y, layer) == EntityEngine.InvalidInstanceID)
                return true;
            else
                return false;
        }

        public override void Update(GameTime gameTime)
        {
            //Do Nothing
        }

        public override void Draw(GameTime gameTime, SpriteBatch sb)
        {
            //Do Nothing
        }
    }
}