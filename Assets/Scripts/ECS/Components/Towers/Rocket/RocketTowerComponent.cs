using System;
using UnityEngine;

namespace ECS.Components.Towers.Rocket
{
    [Serializable]
    public struct RocketTowerComponent
    {
        public float damage;
        public float explosionRadius;
        public float attacksPerSecond;
        public float rocketVelocity;

        public Transform projectilePoint;
    }
}