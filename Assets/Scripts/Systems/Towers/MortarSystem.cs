using Components;
using Components.Core;
using Components.Factory;
using Components.Towers;
using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class MortarSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly StaticData _staticData = null;

        readonly EcsFilter<PositionComponent, MortarTurretComponent, FireProgressComponent> mortarFilter = null;
        readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> enemyFilter = null;

        public void Run()
        {   
            foreach (var towerIndex in mortarFilter)
            {
                ref var fireProgress = ref mortarFilter.Get3(towerIndex);
                ref var tower = ref mortarFilter.Get2(towerIndex);
                fireProgress.progress += tower.attackSpeed * Time.deltaTime;

                List<int> enemiesInRange = new();
                ref var towerPosition = ref mortarFilter.Get1(towerIndex).transform;
                ref var towerRadius = ref tower.attackRadius;
                ref var innerRadius = ref tower.innerRadius;
                int closestEnemyIndex = -1;
                foreach (var enemyIndex in enemyFilter)
                {
                    ref var enemyPosition = ref enemyFilter.Get1(enemyIndex).transform;
                    var distance = Vector3.Distance(towerPosition.position, enemyPosition.position);
                    if (distance < towerRadius && distance > innerRadius)
                    {
                        enemiesInRange.Add(enemyIndex);
                    }
                }
                if (enemiesInRange.Count > 0) closestEnemyIndex = enemiesInRange.Min();

                while (fireProgress.progress >= 1f)
                {
                    if (closestEnemyIndex >= 0 && fireProgress.progress >= 1f)
                    {
                        ref var enemy = ref enemyFilter.GetEntity(closestEnemyIndex);
                        Shoot(tower, enemy);
                        fireProgress.progress -= 1f;
                    }
                    else
                    {
                        fireProgress.progress = 0.999f;
                    }
                }
            }
        }

        void Shoot(MortarTurretComponent tower, EcsEntity enemy)
        {
            float targetingRange = tower.attackRadius;
            float x_speed = targetingRange + 0.25001f;
            float y_speed = -tower.turret.position.y;
            float launchSpeed = Mathf.Sqrt(9.81f * (y_speed + Mathf.Sqrt(x_speed * x_speed + y_speed * y_speed)));

            Vector3 launchPoint = tower.turret.position;
            Vector3 targetPoint = enemy.Get<PositionComponent>().transform.position;
            targetPoint.y = 0f;

            Vector2 dir;
            dir.x = targetPoint.x - launchPoint.x;
            dir.y = targetPoint.z - launchPoint.z;
            float x = dir.magnitude;
            float y = -launchPoint.y;
            dir /= x;

            float g = 9.81f;
            float s = launchSpeed;
            float s2 = s * s;

            float r = s2 * s2 - g * (g * x * x + 2f * y * s2);
            float tanTheta = (s2 + Mathf.Sqrt(r)) / (g * x);
            float cosTheta = Mathf.Cos(Mathf.Atan(tanTheta));
            float sinTheta = cosTheta * tanTheta;

            tower.turret.localRotation = Quaternion.LookRotation(new Vector3(dir.x, tanTheta, dir.y));

            var projectile = _world.NewEntity();
            projectile.Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
             {
                 Prefab = _staticData.projectilePrefab,
                 Position = launchPoint,
                 Rotation = tower.turret.localRotation,
                 Parent = null
             };

            projectile.Get<ProjectileComponent>() = new ProjectileComponent
            {
                launchPoint = launchPoint,
                targetPoint = targetPoint,
                launchVelocity = new Vector3(s * cosTheta * dir.x, s * sinTheta, s * cosTheta * dir.y),
                explosionRadius = tower.explosionRadius,
                damage = tower.damage,
                age = 0
            };
        }
    }
}
