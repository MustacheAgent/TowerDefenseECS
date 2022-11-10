using System;
using UnityEngine;

namespace Components.Towers
{
    [Serializable]
    public struct RocketTowerComponent
    {
        public float damage;
        public float explosionRadius;
        public float attackSpeed;

        public Transform projectilePoint;
        public Transform turret;
    }
}