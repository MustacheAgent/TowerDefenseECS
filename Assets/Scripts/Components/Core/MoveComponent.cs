using System;
using UnityEngine;

namespace Components.Core
{
    [Serializable]
    public struct MoveComponent
    {
        public Rigidbody rigidbody;
        public float speed;
    }
}
