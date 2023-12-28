using Components.Factory;
using Factories;
using Leopotam.Ecs;
using Services;
using UnityEngine;
using Voody.UniLeo;

namespace Systems.Factory
{
    public class GameObjectSpawnSystem : IEcsPreInitSystem
    {
		private readonly SceneData _sceneData;

		private GameObjectFactory _factory;

		public void PreInit()
		{
			_factory = _sceneData.gameObjectFactory;
			
			var convertableGameObjects =
				GameObject.FindObjectsOfType<ConvertToEntity>();
			// Iterate throught all gameobjects, that has ECS Components
			foreach (var convertable in convertableGameObjects)
			{
				_factory.CreateEntity(convertable.gameObject);
			}
		}

		/*
		public void Run()
        {
			if (!_spawnFilter.IsEmpty())
			{
				foreach (int index in _spawnFilter)
				{
					ref EcsEntity spawnEntity = ref _spawnFilter.GetEntity(index);
					var spawnPrefabData = spawnEntity.Get<SpawnPrefabComponent>();
					var gameObject = _factory.Create(spawnPrefabData);
					spawnEntity.Del<SpawnPrefabComponent>();

					if (!spawnEntity.IsNull() && spawnEntity.IsAlive()) 
						gameObject.GetComponent<ConvertToEntity>().SetInitialValues(spawnEntity);
				}
			}
		}
		*/
    }
}
