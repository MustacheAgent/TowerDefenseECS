using System.Collections.Generic;
using System.Linq;
using Components.Core;
using Components.Towers;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class TrackTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, EnemyTag> _enemyFilter = null;
        private readonly EcsFilter<PositionComponent, TowerRadiusComponent> _towerFilter = null;
        
        public void Run()
        {
            foreach (var towerIndex in _towerFilter)
            {
                ref var target = ref _towerFilter.GetEntity(towerIndex).Get<TrackTargetComponent>();
                
                ref var tower = ref _towerFilter.Get1(towerIndex);
                ref var radius = ref _towerFilter.Get2(towerIndex);
                int enemyIndex = GetClosestEnemy(tower.transform, radius.attackRadius, radius.innerRadius);

                if (enemyIndex > -1)
                    target.entity = _enemyFilter.GetEntity(enemyIndex);
                else
                    target.entity = null;
            }
            
            Physics.SyncTransforms();
        }

        private int GetClosestEnemy(Transform towerPosition, float outerRadius, float innerRadius)
        {
            int closestEnemyIndex = -1;
            List<int> enemiesInRange = new();
            foreach (var enemyIndex in _enemyFilter)
            {
                ref var enemyPosition = ref _enemyFilter.Get1(enemyIndex).transform;
                var distance = Vector3.Distance(towerPosition.position, enemyPosition.position);
                if (distance < outerRadius && distance > innerRadius)
                {
                    enemiesInRange.Add(enemyIndex);
                }
            }
            if (enemiesInRange.Count > 0) closestEnemyIndex = enemiesInRange.Min();
            return closestEnemyIndex;
        }
    }
}