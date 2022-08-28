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
    public class LaserSystem : IEcsRunSystem
    {
        readonly EcsWorld world = null;

        readonly EcsFilter<PositionComponent, LaserTurretComponent> laserFilter = null;
        readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> enemyFilter = null;

        public void Run()
        {
            List<int> enemiesInRange = new();
            foreach(var towerIndex in laserFilter)
            {
                ref var towerPosition = ref laserFilter.Get1(towerIndex).transform;
                ref var towerRadius = ref laserFilter.Get2(towerIndex).attackRadius;
                int closestEnemyIndex = -1;
                foreach(var enemyIndex in enemyFilter)
                {
                    ref var enemyPosition = ref enemyFilter.Get1(enemyIndex).transform;
                    if (Vector3.Distance(towerPosition.position, enemyPosition.position) < towerRadius)
                    {
                        enemiesInRange.Add(enemyIndex);
                    }
                    if (enemiesInRange.Count > 0) closestEnemyIndex = enemiesInRange.Min();
                }

            }
        }

        void Shoot()
        {

        }
    }
}
