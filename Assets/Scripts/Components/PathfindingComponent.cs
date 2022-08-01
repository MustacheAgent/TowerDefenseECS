using System;

namespace Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int x;
        public int y;

        public int index;

        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }

        public bool IsWalkable { get; set; }

        public bool IsProcessed { get; set; }
    }
}