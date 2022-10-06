using Components.Core;
using Components.Factory;
using Events;
using Events.Enemies;
using Leopotam.Ecs;

namespace Systems.Factory
{
    public class EntityDestroySystem : IEcsRunSystem
    {
        EcsFilter<GameObjectComponent, DestroyEvent> _enemyDestroyFilter = null;
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
