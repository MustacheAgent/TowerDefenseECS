using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Components
{
    public struct PathComponent
    {
        public List<int2> Path;
        public int PathIndex;
        public Vector3 CurrentDestination;
    }
}
