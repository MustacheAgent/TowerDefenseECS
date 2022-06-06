using System;
using UnityEngine;

namespace Components
{
    [Serializable]
    public struct MoveComponent
    {
        public CharacterController rigidbody;
        public float speed;
    }
}
