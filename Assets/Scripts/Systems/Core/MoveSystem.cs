using Components;
using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class MoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent, PathComponent, EnemyTag> enemyFilter = null;
        readonly EcsFilter<PositionComponent, DestinationTag> destFilter = null;
        SceneData _sceneData;

        public void Run()
        {
            foreach (var i in enemyFilter)
            {
                ref Transform position = ref enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref enemyFilter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.controller;

                ref Transform destination = ref destFilter.Get1(0).transform;

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