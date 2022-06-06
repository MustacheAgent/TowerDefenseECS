using Assets.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.Systems
{
    sealed class MoveSystem : IEcsRunSystem 
    {
        readonly EcsWorld _world = null;
        EcsFilter<PositionComponent, MoveComponent> filter = null;
        
        void IEcsRunSystem.Run() 
        {
            foreach (var i in filter)
            {
                ref MoveComponent moveComponent = ref filter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.rigidbody;
                ref Transform position = ref filter.Get1(i).position;

                Vector3 newPosition = (position.forward);

                controller.Move(speed * Time.deltaTime * newPosition);
            }
        }
    }
}