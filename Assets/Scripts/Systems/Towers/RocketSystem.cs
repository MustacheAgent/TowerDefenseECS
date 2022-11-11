using System.Collections.Generic;
using System.Linq;
using Components;
using Components.Core;
using Components.Towers;
using Leopotam.Ecs;
using Services;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class RocketSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly StaticData _staticData = null;

        private readonly EcsFilter<RocketTowerComponent, FireProgressComponent, TrackTargetComponent> _rocketFilter = null;
        
        public void Run()
        {
            foreach (var towerIndex in _rocketFilter)
            {
                ref var fireProgress = ref _rocketFilter.Get2(towerIndex);
                ref var tower = ref _rocketFilter.Get1(towerIndex);
                fireProgress.progress += tower.attackSpeed * Time.deltaTime;

                ref var enemy = ref _rocketFilter.Get3(towerIndex).entity;
                if (!enemy.HasValue) continue;
                
                ref var enemyPos = ref enemy.Value.Get<TargetComponent>().target;
                TrackTarget(tower, enemyPos);
                while (fireProgress.progress >= 1f)
                {
                    if (fireProgress.progress >= 1f)
                    {
                        Shoot(tower, enemyPos);
                        fireProgress.progress -= 1f;
                    }
                    else
                    {
                        fireProgress.progress = 0.999f;
                    }
                }
            }
        }

        private void TrackTarget(RocketTowerComponent tower, Transform enemy)
        {
            tower.turret.LookAt(enemy.position);
            /*
            var targetPoint = enemy.position;
            var launchPoint = tower.projectilePoint.position;
            Vector3 dir = targetPoint - launchPoint;
            tower.turret.rotation = Quaternion.LookRotation(dir, Vector3.up).normalized;
            */
        }

        private void Shoot(RocketTowerComponent tower, Transform enemy)
        {
            
        }
    }
}