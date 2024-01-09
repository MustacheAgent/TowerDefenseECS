using ECS.Components;
using ECS.Components.Core;
using ECS.Components.Factory;
using ECS.Components.Towers;
using ECS.Events.Enemies;
using Effects;
using Factories;
using Leopotam.Ecs;
using Services;
using UnityEngine;

namespace ECS.Systems.Towers
{
    public class MachineGunSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly FactoryData _factoryData = null;
        private GameObjectFactory _factory;
        
        private readonly EcsFilter<MachineGunComponent, TrackTargetComponent> _machineGunFilter = null;
        
        public void Init()
        {
            _factory = _factoryData.factory;
        }
        
        public void Run()
        {
            foreach (var towerIndex in _machineGunFilter)
            {
                ref var tower = ref _machineGunFilter.GetEntity(towerIndex);
                ref var target = ref tower.Get<TrackTargetComponent>();

                if (!target.Target.HasValue || tower.Has<TimerComponent>() || !target.canAttack)
                {
                    continue;
                }
                
                ref var towerInfo = ref tower.Get<MachineGunComponent>();
                var damage = towerInfo.damage;
                var attackSpeed = towerInfo.attacksPerSecond;
                
                tower.Get<DamageEvent>() = new DamageEvent
                {
                    Target = target.Target.Value,
                    Damage = damage
                };
                
                SpawnTrail(towerInfo.bulletTrail, towerInfo.effectOrigin, target.Target.Value.Get<PositionComponent>().transform);

                tower.Get<TimerComponent>() = new TimerComponent
                {
                    Cooldown = 1 / attackSpeed
                };
            }
        }

        private void SpawnTrail(GameObject trail, Transform start, Transform end)
        {
            var spawn = new SpawnPrefabComponent
            {
                prefab = trail,
                position = start.position,
                rotation = start.rotation,
                parent = null
            };
            
            var bulletTrail = _factory.CreateObject(spawn).GetComponent<BulletTrail>();
            bulletTrail.end = end.position;
        }
    }
}