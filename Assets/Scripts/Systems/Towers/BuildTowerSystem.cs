using Components;
using Components.Core;
using Components.Factory;
using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using Scripts;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class BuildTowerSystem : IEcsRunSystem
    {
        private StaticData _staticData = null;
        private EcsWorld _world = null;
        private PlayerInputData _input = null;
        readonly EcsFilter<EnemyTag> enemyFilter = null;

        public void Run()
        {
            if (_input.leftMousePressed)
            {
                var ray = Camera.main.ScreenPointToRay(_input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit))
                {
                    var gameObject = hit.transform.gameObject;
                    var tile = gameObject.GetEntity();

                    if (tile.Has<TileContentComponent>())
                    {
                        ref var tileContent = ref tile.Get<TileContentComponent>();
                        if (tileContent.content == TileContent.Empty)
                        {   
                            ref var spawnPosition = ref tile.Get<PositionComponent>().transform;
                            _world.NewEntity().Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
                            {
                                Prefab = _staticData.wallPrefab,
                                Position = spawnPosition.position,
                                Rotation = Quaternion.identity,
                                Parent = null
                            };

                            tileContent.content = TileContent.Wall;
                            tile.Get<PathfindingComponent>().isWalkable = false;

                            foreach(var i in enemyFilter)
                            {
                                enemyFilter.GetEntity(i).Get<FindPathEvent>();
                            }
                        }
                    }
                }
            }
        }
    }
}
