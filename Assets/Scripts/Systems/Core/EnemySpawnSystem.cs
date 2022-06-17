using Components;
using Components.Core;
using Leopotam.Ecs;
using System;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class EnemySpawnSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorld _world = null;
        private StaticData _staticData;
        private SceneData _sceneData;

        private float _spawnDelay;
        private float _lastTime;

        readonly EcsFilter<PositionComponent, SpawnTag> tileFilter = null;

        public void Init()
        {
            _spawnDelay = 2.0f;
        }

        public void Run()
        {
            _lastTime += Time.deltaTime;
            if (_lastTime > _spawnDelay)
            {
                foreach (var i in tileFilter)
                {
                    Debug.Log("Spawning!");
                    ref Transform spawnPosition = ref tileFilter.Get1(i).transform;
                    _world.NewEntity().Get<SpawnPrefabComponent>() = new SpawnPrefabComponent
                    {
                        Prefab = _staticData.enemyPrefab,
                        Position = spawnPosition.position,
                        Rotation = Quaternion.identity,
                        Parent = null
                    };
                }
                _lastTime -= _spawnDelay;
            }
        }
    }
}
