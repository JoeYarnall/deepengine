using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DeepSpace
{
    public class GameSystem : DeepEngine.System
    {
        public GameSystem()
            :base(true, false, GameSystemIds.Game, Aspect.GetListForAll(typeof(CGameState)), new Dictionary<int,MessageHandler>())
        {
        }

        public override void Update(GameTime gameTime)
        {
 	        
        }

        public override void  Draw(GameTime gameTime, SpriteBatch sb)
        {
 	        
        }
    }
}
