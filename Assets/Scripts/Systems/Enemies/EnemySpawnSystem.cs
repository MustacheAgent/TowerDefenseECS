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
        private EcsWorld _world = null;
        private StaticData _staticData = null;

        private float _spawnDelay;
        private float _lastTime;

        readonly EcsFilter<PositionComponent, SpawnTag>.Exclude<DestinationTag> tileFilter = null;
        readonly EcsFilter<SpawnEnemyEvent> _spawnEventFilter = null;

        public void Init()
        {
            
        }

        public void Run()
        {
            foreach(var eventIndex in _spawnEventFilter)
            {
                ref var spawnEvent = ref _spawnEventFilter.Get1(eventIndex);
                
                foreach (var i in tileFilter)
                {
                    ref Transform spawnPosition = ref tileFilter.Get1(i).transform;
                    var entity = _world.NewEntity();
                    entity.Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
                    {
                        Prefab = _staticData.enemies[spawnEvent.type],
                        Position = spawnPosition.position,
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
