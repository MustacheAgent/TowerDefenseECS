using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [Serializable]
    public struct PathfindingComponent
    {
        public int hCost;
        public int gCost;

        public int fCost { get { return hCost + gCost; } }
    }
}