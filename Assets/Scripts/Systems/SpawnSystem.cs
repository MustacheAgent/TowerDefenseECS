using Assets.Scripts.Components;
using Assets.Scripts.Factories;
using Leopotam.Ecs;

namespace Assets.Scripts.Systems
{
    public class SpawnSystem : IEcsPreInitSystem, IEcsRunSystem
	{
		private EcsWorld _world;
		private SceneData _sceneData;

		private EcsFilter<SpawnPrefabComponent> _spawnFilter = null;

		private PrefabFactory _factory;

		public void PreInit()
		{
			_factory = _sceneData.Factory;
			_factory.SetWorld(_world);
		}

		public void Run()
		{
			if (_spawnFilter.IsEmpty())
			{
				return;
			}

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
