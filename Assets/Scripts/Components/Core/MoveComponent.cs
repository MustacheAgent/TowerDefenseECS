using System;
using Pathfinding;
using UnityEngine;

namespace Components.Core
{
    [Serializable]
    public struct MoveComponent
    {
        public CharacterController controller;
        public Rigidbody rigidbody;
        public float speed;
        [HideInInspector] public Tile nextTile;
    }
}
