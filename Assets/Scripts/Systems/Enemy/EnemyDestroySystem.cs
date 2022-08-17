using Components.Core;
using Components.Factory;
using Events.Enemies;
using Leopotam.Ecs;
using Tags;

namespace Systems.Enemy
{
    public class EnemyDestroySystem : IEcsRunSystem
    {
        EcsFilter<GameObjectComponent, EnemyTag, DestroyEvent> _enemyDestroyFilter = null;
        private EcsWorld _world = null;

        public void Run()
        {
            foreach(var i in _enemyDestroyFilter)
            {
                ref var destroyObject = ref _enemyDestroyFilter.Get1(i);

                _world.NewEntity().Get<DestroyPrefabComponent>() = new DestroyPrefabComponent
                {
                    gameObject = destroyObject.gameObject
                };

                _enemyDestroyFilter.GetEntity(i).Destroy();
            }
        }
    }
}
