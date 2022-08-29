using System;
using UnityEngine;

namespace Components.Towers
{
    [Serializable]
    public struct LaserTurretComponent
    {
        public Transform turret;
        public Transform laser;

        public float damagePerSecond;
        public int attackRadius;
    }
}
