using Components.Core;
using Events;
using Factories;
using Leopotam.Ecs;
using Services;

namespace Systems.Factory
{
    public class GameObjectDestroySystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly FactoryData _factoryData;

        private GameObjectFactory _factory;

        //private EcsFilter<DestroyPrefabComponent> _destroyFilter = null;
        readonly EcsFilter<GameObjectComponent, DestroyEvent> _destroyFilter = null;

        public void PreInit()
        {
            _factory = _factoryData.factory;
        }

        public void Run()
        {
            if (!_destroyFilter.IsEmpty())
            {
                foreach (int index in _destroyFilter)
                {
                    ref var destroyObject = ref _destroyFilter.Get1(index).gameObject;
                    _factory.Destroy(destroyObject);
                    _destroyFilter.GetEntity(index).Destroy();
                }
            }
        }
    }
}
