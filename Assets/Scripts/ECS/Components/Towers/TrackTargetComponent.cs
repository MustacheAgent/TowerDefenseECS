using System;
using Leopotam.Ecs;

namespace ECS.Components.Towers
{
    [Serializable]
    public struct TrackTargetComponent
    {
        public float attackRadius;
        public float innerRadius;
        
        public EcsEntity? entity;
    }
}