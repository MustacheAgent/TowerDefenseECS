using Components;
using Components.Core;
using Components.Towers;
using Events;
using Events.Enemies;
using Leopotam.Ecs;
using System.Collections.Generic;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class ProjectileSystem : IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, ProjectileComponent> projectileFilter = null;
        readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> enemyFilter = null;
        readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var index in projectileFilter)
            {
                ref var transform = ref projectileFilter.Get1(index).transform;
                ref var projectile = ref projectileFilter.Get2(index);
                
                ref float age = ref projectile.age;

                age += Time.deltaTime;
                Vector3 p = projectile.launchPoint + projectile.launchVelocity * age;
                p.y -= 0.5f * 9.81f * age * age;

                if (p.y <= 0f)
                {
                    Explode(index);
                    projectileFilter.GetEntity(index).Get<DestroyEvent>();
                    continue;
                }

                transform.localPosition = p;
                Vector3 d = projectile.launchVelocity;
                d.y -= 9.81f * age;
                transform.localRotation = Quaternion.LookRotation(d);
            }
        }

        private void Explode(int index)
        {
            foreach (var enemyIndex in enemyFilter)
            {
                ref var enemyPosition = ref enemyFilter.Get1(enemyIndex).transform;
                ref var transform = ref projectileFilter.Get1(index).transform;
                ref var projectile = ref projectileFilter.Get2(index);

                var distance = Vector3.Distance(transform.position, enemyPosition.position);
                if (distance < projectile.explosionRadius)
                {
                    _world.NewEntity().Get<DamageEvent>() = new DamageEvent
                    {
                        entity = enemyFilter.GetEntity(enemyIndex),
                        damage = projectile.damage
                    };
                }
            }
        }
    }
}
