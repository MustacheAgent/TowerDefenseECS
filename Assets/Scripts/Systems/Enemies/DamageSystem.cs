using Components;
using Events;
using Events.Enemies;
using Leopotam.Ecs;

namespace Systems.Enemies
{
    public class DamageSystem : IEcsRunSystem
    {
        readonly EcsFilter<DamageEvent> _damageFilter = null;

        public void Run()
        {
            foreach(var index in _damageFilter)
            {
                ref var damageEvent = ref _damageFilter.Get1(index);
                ref EcsEntity entity = ref damageEvent.entity;
                ref var health = ref entity.Get<HealthComponent>().health;
                ref var damage = ref damageEvent.damage;
                health -= damage;
                if (health < 0)
                {
                    entity.Get<DestroyEvent>();
                }
                _damageFilter.GetEntity(index).Del<DamageEvent>();
            }
        }
    }
}
