﻿using System;
using Effects;
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
        public GameObject bulletTrail;
        public ParticleSystem muzzleFlash;
    }
}