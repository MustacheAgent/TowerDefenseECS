﻿using System;
using UnityEngine;

namespace Components.Towers
{
    [Serializable]
    public struct MortarTurretComponent
    {
        public float damage;
        public float attackRadius;
        public float innerRadius;
        public float explosionRadius;
        public float attackSpeed;

        public Transform turret;
    }
}
