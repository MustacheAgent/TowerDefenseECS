using Components;
using Components.Core;
using Components.Factory;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Enemy
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private StaticData _staticData = null;

        private float _spawnDelay;
        private float _lastTime;

        readonly EcsFilter<PositionComponent, SpawnTag>.Exclude<DestinationTag> tileFilter = null;

        public void Init()
        {
            _spawnDelay = 4.0f;
            _lastTime = _spawnDelay;
        }

        public void Run()
        {
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
                    entity.Get<HealthComponent>() = new HealthComponent
                    {
                        health = 50
                    };
                }
                _lastTime -= _spawnDelay;
            }
        }
    }
}
