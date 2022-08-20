using Components;
using Components.Core;
using Events.Enemies;
using Leopotam.Ecs;
using Scripts;
using Tags;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Enemy
{
    public class EnemyMoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent, PathComponent, EnemyTag> enemyFilter = null;
        readonly EcsFilter<PositionComponent, DestinationTag> destFilter = null;
        readonly SceneData _sceneData = null;

        public void Run()
        {
            foreach (var i in enemyFilter)
            {
                ref Transform position = ref enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref enemyFilter.Get2(i);
                ref PathComponent path = ref enemyFilter.Get3(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.controller;

                ref int currentPathIndex = ref path.pathIndex;
                if (currentPathIndex >= path.path.Count)
                {
                    enemyFilter.GetEntity(i).Get<DestroyEvent>();
                }
                else
                {
                    int2 currentPathXY = path.path[currentPathIndex];

                    EcsEntity nextOnPath =
                        _sceneData.tiles[PathfindingExtensions.CalculateIndex(currentPathXY.x, currentPathXY.y, _sceneData.gridSizeX)];
                    ref Transform destination = ref nextOnPath.Get<PositionComponent>().transform;

                    //ref Transform destination = ref destFilter.Get1(0).transform;

                    if (Vector2.Distance(new Vector2(position.position.x, position.position.z),
                        new Vector2(destination.position.x, destination.position.z)) > .1f)
                    {
                        Vector3 newPosition = (destination.position - position.position).normalized;
                        controller.Move(speed * Time.deltaTime * newPosition);
                        //position.Translate(speed * Time.deltaTime * newPosition);
                    }
                    else
                    {
                        currentPathIndex++;
                    }
                }
            }
        }
    }
}