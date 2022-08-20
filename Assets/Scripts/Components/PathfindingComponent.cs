using System;
using Unity.Mathematics;

namespace Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int2 position;

        public int index;
        public int cameFromIndex;

        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }

        public bool isWalkable;
    }
}