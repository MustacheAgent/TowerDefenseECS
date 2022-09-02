using Components;
using Events.Enemies;
using Leopotam.Ecs;
using Tags;

namespace Systems.Core
{
    public class DamageSystem : IEcsRunSystem
    {
        readonly EcsFilter<DamageEvent> damageFilter = null;

        public void Run()
        {
            foreach(var index in damageFilter)
            {
                ref var damageEvent = ref damageFilter.Get1(index);
                ref EcsEntity entity = ref damageEvent.entity;
                ref var health = ref entity.Get<HealthComponent>().health;
                ref var damage = ref damageEvent.damage;
                health -= damage;
                if (health < 0)
                {
                    entity.Get<DestroyEvent>();
                }
                damageFilter.GetEntity(index).Del<DamageEvent>();
            }
        }
    }
}
