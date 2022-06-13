using System;
using UnityEngine;

namespace Components.Core
{
    [Serializable]
    public struct MoveComponent
    {
        public CharacterController rigidbody;
        public float speed;
    }
}
