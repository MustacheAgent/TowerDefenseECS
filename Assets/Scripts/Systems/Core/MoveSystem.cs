using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class MoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent> filter = null;
        readonly EcsFilter<PositionComponent, DestinationTag> tileFilter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref Transform position = ref filter.Get1(i).transform;
                ref MoveComponent moveComponent = ref filter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.controller;
                ref Rigidbody rigidbody = ref moveComponent.rigidbody;

                ref Transform destination = ref tileFilter.Get1(0).transform;

                Vector3 newPosition = (destination.position - position.position).normalized;

                if (newPosition != Vector3.zero)
                {
                    controller.Move(speed * Time.deltaTime * newPosition);
                    //rigidbody.velocity = speed * Time.deltaTime * destination.position;
                }
            }
        }
    }
}