using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int hCost;
        public int gCost;
        public bool isWalkable;

        public int fCost { get { return hCost + gCost; } }
    }
}