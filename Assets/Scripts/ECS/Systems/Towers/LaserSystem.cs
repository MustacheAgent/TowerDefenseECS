using ECS.Components;
using ECS.Components.Core;
using ECS.Components.Towers;
using ECS.Events.Enemies;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class LaserSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LaserTurretComponent, TrackTargetComponent> _laserTowerFilter = null;

        public void Run()
        {
            foreach (var towerIndex in _laserTowerFilter)
            {
                ref var tower = ref _laserTowerFilter.GetEntity(towerIndex);
                
                ref var target = ref tower.Get<TrackTargetComponent>();
                ref var towerInfo = ref tower.Get<LaserTurretComponent>();

                if (target.Target == null)
                { 
                    towerInfo.laser.localScale = Vector3.zero;
                    continue;
                }

                if (target.canAttack)
                {
                    LaserBeam(towerInfo, target.Target.Value, target.turret);
                
                    if (tower.Has<TimerComponent>()) continue;
                
                    var damage = towerInfo.damage;
                    var attackSpeed = towerInfo.attacksPerSecond;
                
                    tower.Get<DamageEvent>() = new DamageEvent
                    {
                        Target = target.Target.Value,
                        Damage = damage
                    };

                    tower.Get<TimerComponent>() = new TimerComponent
                    {
                        Cooldown = 1 / attackSpeed
                    };
                }
                else
                {
                    towerInfo.laser.localScale = Vector3.zero;
                }
            }
        }

        private void LaserBeam(LaserTurretComponent tower, EcsEntity enemy, Transform turret)
        {
            ref var laserBeam = ref tower.laser;
            ref var enemyPosition = ref enemy.Get<PositionComponent>().transform;

            laserBeam.localRotation = turret.localRotation;
            var d = Vector3.Distance(turret.position, enemyPosition.position);
            var laserScale = laserBeam.localScale;
            laserScale.x = 0.1f;
            laserScale.y = 0.1f;
            laserScale.z = d;
            laserBeam.localScale = laserScale;
            laserBeam.localPosition = turret.localPosition + 0.5f * d * laserBeam.forward;
        }
    }
}
