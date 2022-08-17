using Components;
using Components.Core;
using Events.Enemies;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Enemy
{
    public class EnemyMoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent, PathComponent, EnemyTag> enemyFilter = null;
        readonly EcsFilter<PositionComponent, DestinationTag> destFilter = null;

        public void Run()
        {
            foreach (var i in enemyFilter)
            {
                ref Transform position = ref enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref enemyFilter.Get2(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.controller;

                ref Transform destination = ref destFilter.Get1(0).transform;
                Vector3 newPosition = destination.position - position.position;

                if (Vector3.Distance(position.position, destination.position) > .5f)
                {                    
                    controller.Move(speed * Time.deltaTime * newPosition.normalized);
                }
                else
                {
                    enemyFilter.GetEntity(i).Get<DestroyEvent>();
                }
            }
        }
    }
}