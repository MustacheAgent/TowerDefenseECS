using Components.Factory;
using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(menuName = "Factories/Game Object Factory", fileName = "GameObjectFactory")]
    public class GameObjectFactory : ScriptableObject
    {
        public GameObject Spawn(SpawnPrefabComponent spawnInfo)
        {
            var gameObject = Instantiate(spawnInfo.Prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);

            return gameObject;
        }

        public void Reclaim(GameObject destroyObject)
        {
            Destroy(destroyObject);
        }
    }
}
