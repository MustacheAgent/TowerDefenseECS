using System;
using UnityEngine;

namespace ECS.Components.Enemies
{
    [Serializable]
    public struct TargetComponent
    {
        public Transform target;
    }
}