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
                ref MoveComponent moveComponent = ref filter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.rigidbody;
                ref Transform position = ref filter.Get1(i).transform;

                ref Transform destination = ref tileFilter.Get1(0).transform;

                Vector3 newPosition = (destination.position - position.position).normalized;

                if (newPosition != Vector3.zero)
                {
                    Quaternion toRotation = Quaternion.LookRotation(newPosition, Vector3.up);
                    controller.transform.rotation = Quaternion.RotateTowards(controller.transform.rotation, toRotation, 720 * Time.deltaTime);
                }
                controller.Move(speed * Time.deltaTime * newPosition);
            }
        }
    }
}