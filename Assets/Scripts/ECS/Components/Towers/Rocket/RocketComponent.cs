﻿using UnityEngine;

namespace ECS.Components.Towers.Rocket
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