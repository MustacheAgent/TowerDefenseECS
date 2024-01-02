using Components.Enemies;
using ECS.Components.Enemies;
using Events;
using Events.Enemies;
using Leopotam.Ecs;
using Services;

namespace Systems.Enemies
{
    public class DamageSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<DamageEvent> _damageFilter = null;

        public void Run()
        {
            foreach(var index in _damageFilter)
            {
                ref var damageEvent = ref _damageFilter.Get1(index);
                ref var entity = ref damageEvent.entity;
                ref var health = ref entity.Get<HealthComponent>().health;
                ref var damage = ref damageEvent.damage;
                health -= damage;
                if (health < 0)
                {
                    _world.NewEntity().Get<CurrencyChangedEvent>() = new CurrencyChangedEvent
                    {
                        CurrencyChanged = entity.Get<CurrencyComponent>().killPrice
                    };
                    entity.Get<DestroyEvent>();
                }
                _damageFilter.GetEntity(index).Del<DamageEvent>();
            }
        }
    }
}
