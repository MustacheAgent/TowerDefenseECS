using Leopotam.Ecs;

namespace Events.Enemies
{
    public struct DamageEvent
    {
        public EcsEntity entity;
        public float damage;
    }
}
