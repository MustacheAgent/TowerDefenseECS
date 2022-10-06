using UnityEngine;

namespace Components.Towers
{
    public struct ProjectileComponent
    {
        public float damage;
        public float explosionRadius;
        public float age;

        public Vector3 launchPoint;
        public Vector3 targetPoint;
        public Vector3 launchVelocity;
    }
}
