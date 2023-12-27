using Components.Factory;
using Leopotam.Ecs;
using Events.Enemies;
using UnityEngine;
using Services;

namespace Systems.Enemies
{
    public class EnemySpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly StaticData _staticData = null;
        private readonly GridData _gridData = null;

        private float _spawnDelay;
        private float _lastTime;

        private readonly EcsFilter<SpawnEnemyEvent> _spawnEventFilter = null;

        public void Run()
        {
            foreach(var eventIndex in _spawnEventFilter)
            {
                ref var spawnEvent = ref _spawnEventFilter.Get1(eventIndex);
                
                foreach (var tile in _gridData.spawnTiles)
                {
                    var spawnPosition = tile.transform.position;
                    //spawnPosition.y += 0.2f;
                    var entity = _world.NewEntity();
                    ref var spawn = ref entity.Get<SpawnPrefabComponent>();
                    spawn = new SpawnPrefabComponent
                    {
                        Prefab = _staticData.enemiesDictionary[spawnEvent.type],
                        Position = spawnPosition,
                        Rotation = Quaternion.identity,
                        Parent = null
                    };
                }

                _spawnEventFilter.GetEntity(eventIndex).Destroy();
            }
        }
    }
}
