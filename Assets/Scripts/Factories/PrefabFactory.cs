using Assets.Scripts.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Assets.Scripts.Factories
{
    public class PrefabFactory : MonoBehaviour
    {
        private EcsWorld _world;

        public void SetWorld(EcsWorld world)
        {
            _world = world;
        }

        public void Spawn(SpawnPrefabComponent spawnData)
        {
            GameObject gameObject = Instantiate(spawnData.Prefab, spawnData.Position, spawnData.Rotation, spawnData.Parent);
            /*
            var monoEntity = gameObject.GetComponent<MonoEntity>();
            if (monoEntity == null)
                return;
            EcsEntity ecsEntity = _world.NewEntity();
            monoEntity.Make(ref ecsEntity);
            */
        }
    }
}
