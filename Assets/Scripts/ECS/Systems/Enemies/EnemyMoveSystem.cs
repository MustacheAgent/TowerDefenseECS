using Components.Core;
using ECS.Components.Core;
using ECS.Tags;
using Events;
using Events.Enemies;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Enemies
{
    public class EnemyMoveSystem : IEcsRunSystem 
    {
        private readonly EcsFilter<PositionComponent, MoveComponent, EnemyTag> _enemyFilter = null;

        public void Run()
        {
            foreach (var i in _enemyFilter)
            {
                ref var moveComponent = ref _enemyFilter.Get2(i);
                
                var transform = _enemyFilter.Get1(i).transform;
                var speed = moveComponent.speed;
                var target = moveComponent.nextTile;
                
                if (Vector3.Distance(transform.position, target.transform.position) > 0.05f)
                {
                    transform.position = Vector3.MoveTowards(transform.position, target.transform.position,
                        speed * Time.deltaTime);
                }
                else
                {
                    moveComponent.nextTile = moveComponent.nextTile.next;
                    if (moveComponent.nextTile != null) continue;
                    _enemyFilter.GetEntity(i).Get<DestroyEvent>();
                    _enemyFilter.GetEntity(i).Get<ReachedBaseEvent>();
                }
            }
        }

        private bool CheckDistance(Vector3 position, Vector3 destination)
        {
            return Vector2.Distance(new Vector2(position.x, position.z), new Vector2(destination.x, destination.z)) > 0.1f;
        }
    }
}