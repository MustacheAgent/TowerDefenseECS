using Components;
using Components.Core;
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

        readonly EcsFilter<PositionComponent, MortarTurretComponent> mortarFilter = null;
        readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> enemyFilter = null;

        public void Run()
        {
            List<int> enemiesInRange = new();
            foreach (var towerIndex in mortarFilter)
            {
                ref var towerPosition = ref mortarFilter.Get1(towerIndex).transform;
                ref var tower = ref mortarFilter.Get2(towerIndex);
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

                if (closestEnemyIndex >= 0)
                {
                    ref var enemy = ref enemyFilter.GetEntity(closestEnemyIndex);
                    Shoot(tower, enemy);
                }
                else
                {
                    
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
        }
    }
}
