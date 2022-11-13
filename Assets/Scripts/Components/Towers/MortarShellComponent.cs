using UnityEngine;

namespace Components.Towers
{
    public struct MortarShellComponent
    {
        public float Damage;
        public float ExplosionRadius;
        public float Age;

        public Vector3 LaunchPoint;
        public Vector3 TargetPoint;
        public Vector3 LaunchVelocity;
    }
}
