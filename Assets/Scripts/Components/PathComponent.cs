using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct PathComponent
    {
        public List<int2> path;
        public int pathIndex;
        public Vector3 currentDestination;
    }
}
