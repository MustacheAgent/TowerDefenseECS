using Components.Factory;
using Events.Enemies;
using Factories;
using Leopotam.Ecs;
using Scripts;
using Tags;

namespace Systems.Factory
{
    public class SpawnSystem : IEcsPreInitSystem, IEcsRunSystem
    {
		private readonly SceneData _sceneData;

		private EcsFilter<SpawnPrefabComponent> _spawnFilter = null;

		private GameObjectFactory _factory;

		public void PreInit()
		{
			_factory = _sceneData.factory;
		}

		public void Run()
        {
			if (!_spawnFilter.IsEmpty())
			{
				foreach (int index in _spawnFilter)
				{
					ref EcsEntity spawnEntity = ref _spawnFilter.GetEntity(index);
					var spawnPrefabData = spawnEntity.Get<SpawnPrefabComponent>();
					var gameObject = _factory.Spawn(spawnPrefabData);
					spawnEntity.Del<SpawnPrefabComponent>();
				}
			}
		}
    }
}
