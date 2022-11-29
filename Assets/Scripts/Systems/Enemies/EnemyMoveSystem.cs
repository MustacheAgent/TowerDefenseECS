using Components;
using Components.Core;
using Events;
using Events.Enemies;
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
        private readonly EcsFilter<PositionComponent, MoveComponent, PathComponent, EnemyTag> _enemyFilter = null;
        private readonly PathfindingData _pathfindingData = null;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                ref Transform transform = ref _enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref _enemyFilter.Get2(i);
                ref PathComponent path = ref _enemyFilter.Get3(i);

                ref float speed = ref moveComponent.speed;

                ref int currentPathIndex = ref path.PathIndex;
                if (currentPathIndex >= path.Path.Count)
                {
                    _enemyFilter.GetEntity(i).Get<DestroyEvent>();
                    _enemyFilter.GetEntity(i).Get<ReachedBaseEvent>();
                }
                else
                {
                    int2 currentPathXY = path.Path[currentPathIndex];

                    EcsEntity nextOnPath =
                        _pathfindingData.Tiles[PathfindingExtensions.CalculateIndex(currentPathXY.x, currentPathXY.y, _pathfindingData.gridSizeX)];
                    var destination = nextOnPath.Get<PositionComponent>().transform.position;
                    destination.y = transform.localScale.y;

                    if (CheckDistance(transform.position, destination))
                    {
                        var position = transform.position;
                        
                        Vector3 relativePos = destination - position;
                        Quaternion relativeRot = Quaternion.LookRotation(relativePos);
                        Quaternion enemyRotation = transform.rotation;
                        transform.rotation = Quaternion.Lerp(enemyRotation, relativeRot, 6 * Time.deltaTime);
                        
                        position = Vector3.MoveTowards(position, destination, speed * Time.deltaTime);
                        transform.position = position;
                    }
                    else
                    {
                        currentPathIndex++;
                    }
                }
            }
        }

        private bool CheckDistance(Vector3 position, Vector3 destination)
        {
            return Vector2.Distance(new Vector2(position.x, position.z), new Vector2(destination.x, destination.z)) > 0.1f;
        }
    }
}