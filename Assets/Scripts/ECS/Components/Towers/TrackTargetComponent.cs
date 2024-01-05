using System;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct TrackTargetComponent
    {
        [Header("Radius")]
        public float attackRadius;
        public float innerRadius;
        
        [Header("Rotation")]
        public Transform turret;
        public int rotateSpeed;
        
        public EcsEntity? Target;
        [HideInInspector] public bool canAttack;
    }
}