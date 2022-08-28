using System;
using UnityEngine;

namespace Components.Towers
{
    [Serializable]
    public struct LaserTurretComponent
    {
        public Transform turret;
        public Transform laser;

        public int damagePerSecond;
        public int attackRadius;
    }
}
