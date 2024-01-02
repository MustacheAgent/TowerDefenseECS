using Components.Factory;
using Components.Towers;
using ECS.Components.Enemies;
using ECS.Components.Towers;
using Leopotam.Ecs;
using Services;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class RocketTowerSystem : IEcsRunSystem
    {
        readonly EcsWorld _world = null;
        readonly FactoryData _staticData = null;

        private readonly EcsFilter<RocketTowerComponent, FireProgressComponent, TrackTargetComponent> _rocketFilter = null;
        
        public void Run()
        {
            foreach (var towerIndex in _rocketFilter)
            {
                ref var fireProgress = ref _rocketFilter.Get2(towerIndex);
                ref var tower = ref _rocketFilter.Get1(towerIndex);
                fireProgress.progress += tower.attackSpeed * Time.deltaTime;

                ref var enemy = ref _rocketFilter.Get3(towerIndex).entity;

                var fire = false;
                var enemyPos = Vector3.forward;
                if (enemy.HasValue && enemy.Value.IsAlive())
                {
                    enemyPos = enemy.Value.Get<TargetComponent>().target.position;
                    fire = true;
                }
                
                TrackTarget(tower, enemyPos);
                while (fireProgress.progress >= 1f)
                {
                    if (fireProgress.progress >= 1f && fire)
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

        private void TrackTarget(RocketTowerComponent tower, Vector3 enemyPos)
        {
            Vector3 relativePos = enemyPos - tower.turret.position;
            Quaternion relativeRot = Quaternion.LookRotation(relativePos);
            Quaternion turretRotation = tower.turret.rotation;
            tower.turret.rotation = Quaternion.Lerp(turretRotation, relativeRot, tower.rotationSpeed * Time.deltaTime);
        }

        private void Shoot(RocketTowerComponent tower, Vector3 enemyPos)
        {
            var rocket = _world.NewEntity();

            rocket.Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
            {
                Prefab = _staticData.rocketPrefab,
                Position = tower.projectilePoint.position,
                Rotation = tower.projectilePoint.rotation,
                Parent = null
            };
            rocket.Get<RocketComponent>() = new RocketComponent()
            {
                Damage = tower.damage,
                ExplosionRadius = tower.explosionRadius,
                Velocity = tower.rocketVelocity,
                LaunchPoint = tower.projectilePoint.position,
                TargetPoint = enemyPos
            };
        }
    }
}