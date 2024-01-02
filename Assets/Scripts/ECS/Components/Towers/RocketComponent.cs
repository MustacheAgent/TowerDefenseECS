using UnityEngine;

namespace Components.Towers
{
    public struct RocketComponent
    {
        public float Damage;
        public float ExplosionRadius;
        public float Velocity;
        
        public Vector3 LaunchPoint;
        public Vector3 TargetPoint;
    }
}