using System;
using Pathfinding;
using UnityEngine;

namespace Components.Core
{
    [Serializable]
    public struct MoveComponent
    {
        public float speed;
        [HideInInspector] public Tile nextTile;
    }
}
