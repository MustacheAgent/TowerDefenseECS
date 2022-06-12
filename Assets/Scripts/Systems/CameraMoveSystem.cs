using Components;
using Leopotam.Ecs;
using System;

namespace Systems
{
    public class CameraMoveSystem : IEcsRunSystem
    {
        readonly EcsFilter<PlayerInputComponent> filter = null;

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
