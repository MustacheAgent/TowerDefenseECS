using Components;
using Components.Core;
using Components.Factory;
using Leopotam.Ecs;
using Events.Enemies;
using Tags;
using UnityEngine;
using Dictionaries;
using Services;

namespace Systems.Enemies
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly SceneData _sceneData = null;

        private float _spawnDelay;
        private float _lastTime;

        private readonly EcsFilter<PositionComponent, SpawnTag>.Exclude<DestinationTag> _tileFilter = null;
        private readonly EcsFilter<SpawnEnemyEvent> _spawnEventFilter = null;

        public void Init()
        {
            
        }

        public void Run()
        {
            foreach(var eventIndex in _spawnEventFilter)
            {
                ref var spawnEvent = ref _spawnEventFilter.Get1(eventIndex);
                
                foreach (var i in _tileFilter)
                {
                    var spawnPosition = _tileFilter.Get1(i).transform.position;
                    spawnPosition.y += 0.2f;
                    var entity = _world.NewEntity();
                    entity.Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
                    {
                        Prefab = _sceneData.enemiesDictionary[spawnEvent.type],
                        Position = spawnPosition,
                        Rotation = Quaternion.identity,
                        Parent = null
                    };
                }

                _spawnEventFilter.GetEntity(eventIndex).Destroy();
            }
            /*
            _lastTime += Time.deltaTime;
            if (_lastTime > _spawnDelay)
            {
                foreach (var i in tileFilter)
                {
                    ref Transform spawnPosition = ref tileFilter.Get1(i).transform;
                    var entity = _world.NewEntity();
                    entity.Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
                    {
                        Prefab = _staticData.enemyPrefab,
                        Position = spawnPosition.position,
                        Rotation = Quaternion.identity,
                        Parent = null
                    };
                }
                _lastTime -= _spawnDelay;
            }
            */
        }
    }
}
