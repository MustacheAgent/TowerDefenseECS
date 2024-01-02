using Components.Factory;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Factories
{
    [CreateAssetMenu(menuName = "Factories/Game Object Factory", fileName = "GameObjectFactory")]
    public class GameObjectFactory : ScriptableObject
    {
        public GameObject CreateObjectAndEntity(SpawnPrefabComponent spawnInfo)
        {
            var gameObject = CreateObject(spawnInfo);
            CreateEntity(gameObject);

            return gameObject;
        }
        
        public GameObject CreateObject(SpawnPrefabComponent spawnInfo)
        {
            return Instantiate(spawnInfo.Prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
        }

        public void Destroy(GameObject destroyObject)
        {
            Object.Destroy(destroyObject);
        }
        
        public EcsEntity CreateEntity(GameObject gameObject)
        {
            // Creating new Entity
            var world = WorldHandler.GetWorld();
            var entity = world.NewEntity();
            var convertComponent = gameObject.GetComponent<ConvertToEntity>();
            if (convertComponent)
            {
                foreach (var component in gameObject.GetComponents<Component>())
                {
                    if (component is IConvertToEntity entityComponent)
                    {
                        // Adding Component to entity
                        entityComponent.Convert(entity);
                        GameObject.Destroy(component);
                    }
                }
                
                convertComponent.setProccessed();
                switch (convertComponent.convertMode)
                {
                    case ConvertMode.ConvertAndDestroy:
                        GameObject.Destroy(gameObject);
                        break;
                    case ConvertMode.ConvertAndInject:
                        GameObject.Destroy(convertComponent);
                        break;
                    case ConvertMode.ConvertAndSave:
                        convertComponent.Set(entity);
                        break;
                }
            }

            return entity;
        }
    }
}
