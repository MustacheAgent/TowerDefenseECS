using System.Collections.Generic;
using System.Linq;
using Components.Core;
using Components.Towers;
using ECS.Components.Core;
using ECS.Components.Towers;
using ECS.Tags;
using Leopotam.Ecs;
using Scripts;
using Tags;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class TrackTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, EnemyTag> _enemyFilter = null;

        private const int EnemyLayerMask = 1 << 9;
        
        private readonly EcsFilter<PositionComponent, TrackTargetComponent> _towerFilter = null;
        
        public void Run()
        {
            foreach (var towerIndex in _towerFilter)
            {
                ref var target = ref _towerFilter.Get2(towerIndex);
                ref var tower = ref _towerFilter.Get1(towerIndex);
                
                if (CheckValidTarget(target, tower))
                {
                    continue;
                }
                
                var enemy = GetTarget(tower.transform, target.attackRadius, target.innerRadius);

                if (enemy != null && enemy.Value.IsAlive())
                    target.entity = enemy;
                else
                    target.entity = null;
            }
            
            Physics.SyncTransforms();
        }

        private bool CheckValidTarget(TrackTargetComponent target, PositionComponent tower)
        {
            if (target.entity == null || !target.entity.Value.IsAlive())
            {
                return false;
            }

            var targetPosition = target.entity.Value.Get<PositionComponent>();
            
            var distance = Vector3.Distance(tower.transform.position, targetPosition.transform.position);
            if (distance > target.attackRadius || distance < target.innerRadius)
            {
                return false;
            }

            return true;
        }

        private EcsEntity? GetTarget(Transform towerPosition, float outerRadius, float innerRadius)
        {
            var closestEnemyIndex = -1;
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
            
            if (closestEnemyIndex != -1)
            {
                return _enemyFilter.GetEntity(closestEnemyIndex);
            }
            
            return null;
        }
    }
}