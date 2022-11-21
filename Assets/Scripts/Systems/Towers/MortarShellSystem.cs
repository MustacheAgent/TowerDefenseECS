using Components;
using Components.Core;
using Components.Towers;
using Events;
using Events.Enemies;
using Leopotam.Ecs;
using System.Collections.Generic;
using Components.Enemies;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class MortarShellSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, MortarShellComponent> _projectileFilter = null;
        private readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> _enemyFilter = null;
        private readonly EcsWorld _world = null;

        public void Run()
        {
            foreach (var index in _projectileFilter)
            {
                ref var transform = ref _projectileFilter.Get1(index).transform;
                ref var projectile = ref _projectileFilter.Get2(index);
                
                ref float age = ref projectile.Age;

                age += Time.deltaTime;
                Vector3 p = projectile.LaunchPoint + projectile.LaunchVelocity * age;
                p.y -= 0.5f * 9.81f * age * age;

                if (p.y <= 0f)
                {
                    Explode(index);
                    _projectileFilter.GetEntity(index).Get<DestroyEvent>();
                    continue;
                }

                transform.localPosition = p;
                Vector3 d = projectile.LaunchVelocity;
                d.y -= 9.81f * age;
                transform.localRotation = Quaternion.LookRotation(d);
            }
        }

        private void Explode(int index)
        {
            foreach (var enemyIndex in _enemyFilter)
            {
                ref var enemyPosition = ref _enemyFilter.Get1(enemyIndex).transform;
                ref var transform = ref _projectileFilter.Get1(index).transform;
                ref var projectile = ref _projectileFilter.Get2(index);

                var distance = Vector3.Distance(transform.position, enemyPosition.position);
                if (distance < projectile.ExplosionRadius)
                {
                    _world.NewEntity().Get<DamageEvent>() = new DamageEvent
                    {
                        entity = _enemyFilter.GetEntity(enemyIndex),
                        damage = projectile.Damage
                    };
                }
            }
        }
    }
}
