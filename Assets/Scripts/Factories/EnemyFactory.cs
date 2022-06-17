using Components;
using UnityEngine;

namespace Factories
{
    [CreateAssetMenu(menuName = "Factories/Enemy Factory")]
    public class EnemyFactory : ScriptableObject
    {
        public void Spawn(SpawnPrefabComponent spawnInfo)
        {
            Instantiate(spawnInfo.Prefab, spawnInfo.Position, spawnInfo.Rotation, spawnInfo.Parent);
        }
    }
}
