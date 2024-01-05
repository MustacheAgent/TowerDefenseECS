using Leopotam.Ecs;

namespace ECS.Events.Enemies
{
    public struct DamageEvent
    {
        public EcsEntity Target;
        public float Damage;
    }
}
