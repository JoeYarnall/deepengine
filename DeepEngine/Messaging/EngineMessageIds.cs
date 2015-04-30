using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DeepEngine
{
    public static class EngineMessageIds
    {
        public const int EntityCreated = 0x00000001;
        //SENDS - Nothing
        //RETURNS - Nothing

        public const int EntityDestroyed = 0x00000002;
        //SENDS - Nothing
        //RETURNS - Nothing

        public const int EntityMoved = 0x00000003;
        //SENDS - Nothing
        //RETURNS - Nothing

        public const int GetInstanceIDAt = 0x00000004;
        //SENDS - V1 = (x, y, layer)
        //RETURNS - I1 = EntityId at pos

        public const int GetMouseLocation = 0x00000005;
        //SENDS - Nothing
        //RETURNS - P1 = Mouse Location

        public const int RaycastWorld = 0x00000006;
        //SENDS - P1 = Screen Location To Raycast From 
        //RETURNS - I3 = EntityID of hit
    }
}