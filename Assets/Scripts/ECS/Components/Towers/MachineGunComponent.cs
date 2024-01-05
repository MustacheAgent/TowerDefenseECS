using System;
using UnityEngine;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct MachineGunComponent
    {
        public Transform effectOrigin;
        public int damage;
        public float attacksPerSecond;
    }
}