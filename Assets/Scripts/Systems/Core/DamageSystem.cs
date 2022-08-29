using Components;
using Events.Enemies;
using Leopotam.Ecs;
using Tags;

namespace Systems.Core
{
    public class DamageSystem : IEcsRunSystem
    {
        readonly EcsFilter<DamageEvent, HealthComponent, EnemyTag> enemyFilter = null;

        public void Run()
        {
            foreach(var index in enemyFilter)
            {
                ref var health = ref enemyFilter.Get2(index);
                ref var damage = ref enemyFilter.Get1(index).damage;
                health.health -= damage;
                if (health.health < 0)
                {
                    enemyFilter.GetEntity(index).Get<DestroyEvent>();
                }
                enemyFilter.GetEntity(index).Del<DamageEvent>();
            }
        }
    }
}
