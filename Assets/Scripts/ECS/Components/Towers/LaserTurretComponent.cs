﻿using System;
using UnityEngine;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct LaserTurretComponent
    {
        public Transform laser;

        public float damage;
        public float attacksPerSecond;
    }
}
