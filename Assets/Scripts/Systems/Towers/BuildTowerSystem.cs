using Components;
using Components.Core;
using Components.Factory;
using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using Scripts;
using Services;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class BuildTowerSystem : IEcsRunSystem
    {
        readonly StaticData _staticData = null;
        readonly SceneData _sceneData = null;
        readonly EcsWorld _world = null;
        readonly PlayerInputData _input = null;
        readonly EcsFilter<EnemyTag> enemyFilter = null;

        public void Run()
        {
            if (!_input.leftMousePressed) return;
            var ray = Camera.main.ScreenPointToRay(_input.mousePosition);
            if (!Physics.Raycast(ray, out RaycastHit hit)) return;
            var gameObject = hit.transform.gameObject;
            var tile = gameObject.GetEntity();

            if (!tile.Has<TileContentComponent>()) return;
            ref var tileContent = ref tile.Get<TileContentComponent>();
            if (tileContent.content != TileContent.Empty) return;
            ref var spawnPosition = ref tile.Get<PositionComponent>().transform;
            GameObject prefab = null;
            if (_sceneData.selectedTower != TowerType.None) prefab = _sceneData.towerDictionary[_sceneData.selectedTower];

            if (prefab == null) return;
            _world.NewEntity().Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
            {
                Prefab = prefab,
                Position = spawnPosition.position,
                Rotation = Quaternion.identity,
                Parent = null
            };

            tileContent.content = TileContent.Tower;
            tile.Get<PathfindingComponent>().isWalkable = false;

            foreach (var i in enemyFilter)
            {
                enemyFilter.GetEntity(i).Get<FindPathEvent>();
            }
        }
    }
}
