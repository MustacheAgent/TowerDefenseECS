using Components.Factory;
using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(menuName = "Factories/Game Object Factory")]
    public class GameObjectFactory : ScriptableObject
    {
        public GameObject Spawn(SpawnPrefabComponent spawnInfo)
        {
            return Instantiate(spawnInfo.Prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
        }

        public void Reclaim(DestroyPrefabComponent destroyInfo)
        {
            Destroy(destroyInfo.gameObject);
        }
    }
}
