using Components.Enemies;
using ECS.Components.Core;
using ECS.Components.Towers.Rocket;
using ECS.Events.Enemies;
using Events;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class RocketSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, RocketComponent> _rocketFilter = null;
        private readonly EcsFilter<PositionComponent, HealthComponent> _enemyFilter = null;
        
        public void Run()
        {
            foreach (var rocketIndex in _rocketFilter)
            {
                ref var transform = ref _rocketFilter.Get1(rocketIndex).transform;
                ref var rocket = ref _rocketFilter.Get2(rocketIndex);
                
                var newPos = 
                    Vector3.MoveTowards(transform.position, rocket.TargetPoint, rocket.Velocity * Time.deltaTime);

                if (Vector3.Distance(transform.position, rocket.TargetPoint) <= 0.1f)
                {
                    Explode(rocketIndex);
                    _rocketFilter.GetEntity(rocketIndex).Get<DestroyEvent>();
                    continue;
                }

                transform.position = newPos;
                transform.LookAt(rocket.TargetPoint);
            }
        }

        private void Explode(int index)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref var enemyPosition = ref _enemyFilter.Get1(enemyIndex).transform;
                ref var transform = ref _rocketFilter.Get1(index).transform;
                ref var projectile = ref _rocketFilter.Get2(index);

                var distance = Vector3.Distance(transform.position, enemyPosition.position);
                
                if (!(distance < projectile.ExplosionRadius)) continue;
                var hit = _enemyFilter.GetEntity(enemyIndex);
                hit.Get<DamageEvent>() = new DamageEvent
                {
                    Target = hit,
                    Damage = projectile.Damage
                };
            }
        }
    }
}