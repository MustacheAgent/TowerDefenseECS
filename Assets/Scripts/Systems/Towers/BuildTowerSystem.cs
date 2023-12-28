using Components;
using Components.Core;
using Components.Factory;
using Components.Towers;
using Enums;
using Events;
using Events.Enemies;
using Events.Scenario;
using Leopotam.Ecs;
using MonoProviders.Components.Towers;
using Scripts;
using Services;
using Tags;
using UnityEngine;

namespace Systems.Towers
{
    public class BuildTowerSystem : IEcsRunSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly EcsWorld _world = null;
        private readonly PlayerInputData _input = null;
        private readonly EcsFilter<EnemyTag> _enemyFilter = null;

        private bool _isStarted = false;

        public void Run()
        {
            /*
            if (!_input.leftMousePressed) return;
            if (_input.rightMousePressed)
            {
                _sceneData.selectedTower = TowerType.None;
                return;
            }
            var ray = Camera.main.ScreenPointToRay(_input.mousePosition);
            if (!Physics.Raycast(ray, out var hit)) return;
            var gameObject = hit.transform.gameObject;
            var tile = gameObject.GetEntity();

            if (!tile.Has<TileContentComponent>()) return;
            ref var tileContent = ref tile.Get<TileContentComponent>();
            if (tileContent.content is not TileContent.Empty or TileContent.NonBuildable) return;
            ref var spawnPosition = ref tile.Get<PositionComponent>().transform;
            
            GameObject prefab = null;
            if (_sceneData.selectedTower != TowerType.None) prefab = _sceneData.towerDictionary[_sceneData.selectedTower];
            if (prefab == null) return;
            if (prefab.GetComponent<TowerInfoProvider>().Value.towerPrice > _sceneData.currency)
            {
                _sceneData.selectedTower = TowerType.None;
                return;
            }
            
            _world.NewEntity().Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
            {
                Prefab = prefab,
                Position = spawnPosition.position,
                Rotation = Quaternion.identity,
                Parent = null
            };

            _world.NewEntity().Get<CurrencyChangedEvent>() = new CurrencyChangedEvent
            {
                CurrencyChanged = -prefab.GetComponent<TowerInfoProvider>().Value.towerPrice
            };
            tileContent.content = TileContent.Tower;
            tile.Get<PathfindingComponent>().isWalkable = false;

            foreach (var i in _enemyFilter)
            {
                _enemyFilter.GetEntity(i).Get<FindPathEvent>();
            }

            if (_isStarted) return;
            _world.NewEntity().Get<BeginScenarioEvent>();
            _isStarted = true;
            */
        }
    }
}
