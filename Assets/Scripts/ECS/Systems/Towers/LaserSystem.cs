using Components;
using Components.Core;
using Components.Towers;
using Events.Enemies;
using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using Components.Enemies;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class LaserSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;

        readonly EcsFilter<PositionComponent, LaserTurretComponent> laserFilter = null;
        readonly EcsFilter<PositionComponent, HealthComponent, EnemyTag> enemyFilter = null;

        public void Run()
        {
            List<int> enemiesInRange = new();
            foreach(var towerIndex in laserFilter)
            {
                ref var towerPosition = ref laserFilter.Get1(towerIndex).transform;
                ref var tower = ref laserFilter.Get2(towerIndex);
                ref var towerRadius = ref tower.attackRadius;
                int closestEnemyIndex = -1;
                foreach(var enemyIndex in enemyFilter)
                {
                    ref var enemyPosition = ref enemyFilter.Get1(enemyIndex).transform;
                    var distance = Vector3.Distance(towerPosition.position, enemyPosition.position);
                    if (distance < towerRadius)
                    {
                        enemiesInRange.Add(enemyIndex);
                    }
                }
                if (enemiesInRange.Count > 0) closestEnemyIndex = enemiesInRange.Min();
                ref var laserBeam = ref tower.laser;

                if (closestEnemyIndex >= 0)
                {
                    ref var enemy = ref enemyFilter.GetEntity(closestEnemyIndex);
                    Shoot(tower, enemy);
                }
                else
                {
                    laserBeam.localScale = Vector3.zero;
                }
            }
        }

        void Shoot(LaserTurretComponent tower, EcsEntity enemy)
        {
            ref var laserBeam = ref tower.laser;
            ref var enemyPosition = ref enemy.Get<PositionComponent>().transform;

            tower.turret.LookAt(enemyPosition);

            laserBeam.localRotation = tower.turret.localRotation;
            float d = Vector3.Distance(tower.turret.position, enemyPosition.position);
            Vector3 laserScale = laserBeam.localScale;
            laserScale.x = 0.1f;
            laserScale.y = 0.1f;
            laserScale.z = d;
            laserBeam.localScale = laserScale;
            laserBeam.localPosition = tower.turret.localPosition + 0.5f * d * laserBeam.forward;

            _world.NewEntity().Get<DamageEvent>() = new DamageEvent
            {
                entity = enemy,
                damage = tower.damage * tower.attackSpeed * Time.deltaTime
            };
        }
    }
}
