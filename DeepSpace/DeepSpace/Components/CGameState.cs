using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DeepEngine;

namespace DeepSpace
{
    public class CGameState : Component
    {
        public int MaxNumberOfTurns { get; set; }
        public int CurrentTurnNumber { get; set; }
        public int MaxPlayers { get; set; }
        public int ActivePlayer { get; set; }
        
        public CGameState()
            : base(true, false)
        {
        }
    }
}