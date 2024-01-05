using System;
using UnityEngine;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct MachineGunComponent
    {
        [Header("Parameters")]
        public int damage;
        public float attacksPerSecond;
        
        [Header("VFX")]
        public Transform effectOrigin;
        public ParticleSystem bulletTrail;
    }
}