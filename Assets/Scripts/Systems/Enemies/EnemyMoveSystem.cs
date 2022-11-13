using System.IO;
using Components;
using Components.Core;
using Events;
using Leopotam.Ecs;
using Scripts;
using Services;
using Tags;
using Unity.Mathematics;
using UnityEngine;

namespace Systems.Enemies
{
    public class EnemyMoveSystem : IEcsRunSystem 
    {
        readonly EcsFilter<PositionComponent, MoveComponent, PathComponent, EnemyTag> _enemyFilter = null;
        readonly PathfindingData _pathfindingData = null;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                ref Transform transform = ref _enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref _enemyFilter.Get2(i);
                ref PathComponent path = ref _enemyFilter.Get3(i);

                ref float speed = ref moveComponent.speed;

                ref int currentPathIndex = ref path.pathIndex;
                if (currentPathIndex >= path.path.Count)
                {
                    _enemyFilter.GetEntity(i).Get<DestroyEvent>();
                }
                else
                {
                    int2 currentPathXY = path.path[currentPathIndex];

                    EcsEntity nextOnPath =
                        _pathfindingData.tiles[PathfindingExtensions.CalculateIndex(currentPathXY.x, currentPathXY.y, _pathfindingData.gridSizeX)];
                    var destination = nextOnPath.Get<PositionComponent>().transform.position;
                    destination.y = transform.localScale.y;

                    if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z),
                        new Vector2(destination.x, destination.z)) > 0.5f)
                    {
                        //Vector3 moveDir = (destination.position - transform.position);
                        transform.position = 
                            Vector3.MoveTowards(transform.position, destination, speed * Time.deltaTime);
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