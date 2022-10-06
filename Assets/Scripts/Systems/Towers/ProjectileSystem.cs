using Components.Core;
using Components.Towers;
using Events;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Towers
{
    public class ProjectileSystem : IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, ProjectileComponent> projectileFilter = null;

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
                    projectileFilter.GetEntity(index).Get<DestroyEvent>();
                    continue;
                }

                transform.localPosition = p;
                Vector3 d = projectile.launchVelocity;
                d.y -= 9.81f * age;
                transform.localRotation = Quaternion.LookRotation(d);
            }
        }
    }
}
