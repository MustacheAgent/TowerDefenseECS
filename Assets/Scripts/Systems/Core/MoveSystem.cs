using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    sealed class MoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent> filter = null;
        readonly EcsFilter<PositionComponent, DestinationTag> tileFilter = null;
        
        void IEcsRunSystem.Run() 
        {
            foreach (var i in filter)
            {
                ref Transform position = ref filter.Get1(i).transform;
                ref MoveComponent moveComponent = ref filter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref Rigidbody rigidbody = ref moveComponent.rigidbody;

                ref Transform destination = ref tileFilter.Get1(0).transform;

                Vector3 newPosition = (destination.position - position.position);

                rigidbody.MovePosition(speed * Time.fixedDeltaTime * Vector3.forward.normalized);
            }
        }
    }
}