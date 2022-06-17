using Components;
using Factories;
using Leopotam.Ecs;

namespace Systems.Core
{
    public class SpawnSystem : IEcsPreInitSystem, IEcsRunSystem
    {
		private SceneData _sceneData;

		private EcsFilter<SpawnPrefabComponent> _spawnFilter = null;

		private EnemyFactory _factory;

		public void PreInit()
		{
			_factory = _sceneData.enemyFactory;
		}

		public void Run()
        {
			if (!_spawnFilter.IsEmpty())
			{
				foreach (int index in _spawnFilter)
				{
					ref EcsEntity spawnEntity = ref _spawnFilter.GetEntity(index);
					var spawnPrefabData = spawnEntity.Get<SpawnPrefabComponent>();
					_factory.Spawn(spawnPrefabData);
					spawnEntity.Del<SpawnPrefabComponent>();
				}
			}
		}
    }
}
