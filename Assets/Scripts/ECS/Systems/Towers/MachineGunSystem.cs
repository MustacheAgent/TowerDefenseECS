using ECS.Components;
using ECS.Components.Towers;
using ECS.Events.Enemies;
using Leopotam.Ecs;

namespace ECS.Systems.Towers
{
    public class MachineGunSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MachineGunComponent, TrackTargetComponent> _machineGunFilter = null;

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

                tower.Get<TimerComponent>() = new TimerComponent
                {
                    Cooldown = 1 / attackSpeed
                };
            }
        }
    }
}