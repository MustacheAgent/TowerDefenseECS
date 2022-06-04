using UnityEngine;

namespace Assets.Scripts.Components
{
    public struct PathfindingComponent
    {
        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }
    }
}