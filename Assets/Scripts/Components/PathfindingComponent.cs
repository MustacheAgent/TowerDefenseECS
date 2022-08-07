using System;

namespace Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int x;
        public int z;

        public int index;
        public int cameFromIndex;

        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }

        public bool IsWalkable { get; set; }
    }
}