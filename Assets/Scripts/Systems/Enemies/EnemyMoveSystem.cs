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
                ref Transform position = ref _enemyFilter.Get1(i).transform;
                ref MoveComponent moveComponent = ref _enemyFilter.Get2(i);
                ref PathComponent path = ref _enemyFilter.Get3(i);

                ref float speed = ref moveComponent.speed;
                ref CharacterController controller = ref moveComponent.controller;
                ref Rigidbody rb = ref moveComponent.rigidbody;

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
                    ref Transform destination = ref nextOnPath.Get<PositionComponent>().transform;

                    if (Vector2.Distance(new Vector2(position.position.x, position.position.z),
                        new Vector2(destination.position.x, destination.position.z)) > 0.5f)
                    {
                        Vector3 moveDir = (destination.position - position.position);
                        moveDir.y = 0;
                        //Vector3 projected = Vector3.ProjectOnPlane(moveDir, new Vector3());
                        controller.Move(moveDir.normalized * speed * Time.deltaTime);
                        //rb.MovePosition(rb.position + speed * Time.deltaTime * moveDir);
                        //position.position = Vector3.MoveTowards(position.position, destination.position, speed * Time.deltaTime);
                        //rb.velocity = projected;
                        //position.localPosition = Vector3.LerpUnclamped(position.position, destination.position, speed * Time.deltaTime);
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