using ECS.Components;
using ECS.Components.Core;
using ECS.Components.Factory;
using ECS.Components.Towers;
using ECS.Components.Towers.Rocket;
using Factories;
using Leopotam.Ecs;
using Services;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class RocketTowerSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly FactoryData _factoryData = null;
        private GameObjectFactory _factory;

        private readonly EcsFilter<RocketTowerComponent, TrackTargetComponent> _rocketTowerFilter = null;
        
        public void Init()
        {
            _factory = _factoryData.factory;
        }
        
        public void Run()
        {
            foreach (var towerIndex in _rocketTowerFilter)
            {
                ref var tower = ref _rocketTowerFilter.GetEntity(towerIndex);
                ref var target = ref tower.Get<TrackTargetComponent>();
                
                if (!target.Target.HasValue || tower.Has<TimerComponent>() || !target.canAttack)
                {
                    continue;
                }
                
                ref var towerInfo = ref tower.Get<RocketTowerComponent>();
                var attackSpeed = towerInfo.attacksPerSecond;
                
                Shoot(towerInfo, target.Target.Value.Get<PositionComponent>().transform.position);
                
                tower.Get<TimerComponent>() = new TimerComponent
                {
                    Cooldown = 1 / attackSpeed
                };
            }
        }

        private void Shoot(RocketTowerComponent tower, Vector3 enemyPos)
        {
            var spawn = new SpawnPrefabComponent
            {
                prefab = _factoryData.rocketPrefab,
                position = tower.projectilePoint.position,
                rotation = tower.projectilePoint.rotation,
                parent = null
            };

            var rocket = _factory.CreateObjectAndEntity(spawn);

            var entity = rocket.GetEntity();
            if (entity.HasValue)
            {
                entity.Value.Get<RocketComponent>() = new RocketComponent
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
}