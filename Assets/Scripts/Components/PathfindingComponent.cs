using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }

        public bool IsWalkable { get; set; }
    }
}