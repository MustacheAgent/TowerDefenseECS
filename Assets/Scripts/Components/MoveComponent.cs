using System;
using UnityEngine;

namespace Assets.Scripts.Components
{
    [Serializable]
    public struct MoveComponent
    {
        public CharacterController rigidbody;
        public float speed;
    }
}
