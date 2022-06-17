using Components.Core;
using Components.Towers;
using Leopotam.Ecs;
using System;

namespace Systems.Towers
{
    public class LaserSystem : IEcsRunSystem
    {
        readonly EcsWorld world = null;

        readonly EcsFilter<PositionComponent, LaserTurretComponent> filter = null;

        public void Run()
        {
            
        }

        void Shoot()
        {

        }
    }
}
