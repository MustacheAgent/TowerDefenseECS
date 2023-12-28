using Components.Core;
using Components.Factory;
using Leopotam.Ecs;
using Events.Enemies;
using Factories;
using UnityEngine;
using Services;
using Voody.UniLeo;

namespace Systems.Enemies
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly FactoryData _staticData = null;
        private readonly GridData _gridData = null;

        private GameObjectFactory _factory;
        private float _spawnDelay;
        private float _lastTime;

        private readonly EcsFilter<SpawnEnemyEvent> _spawnEventFilter = null;
        
        public void Init()
        {
            _factory = _staticData.factory;
        }

        public void Run()
        {
            foreach(var eventIndex in _spawnEventFilter)
            {
                ref var spawnEvent = ref _spawnEventFilter.Get1(eventIndex);
                
                foreach (var tile in _gridData.spawnTiles)
                {
                    var spawnPosition = tile.transform.position;
                    //spawnPosition.y += 0.2f;
                    var spawn = new SpawnPrefabComponent
                    {
                        Prefab = _staticData.enemiesDictionary[spawnEvent.type],
                        Position = spawnPosition,
                        Rotation = Quaternion.identity,
                        Parent = null
                    };

                    var enemy = _factory.CreateObjectAndEntity(spawn);
                    var enemyEntity = enemy.GetComponent<ConvertToEntity>().TryGetEntity();
                    if (enemyEntity != null)
                        enemyEntity.Value.Get<MoveComponent>().nextTile = tile;
                }

                _spawnEventFilter.GetEntity(eventIndex).Destroy();
            }
        }

        
    }
}
