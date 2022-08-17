using Components.Factory;
using Factories;
using Leopotam.Ecs;

namespace Systems.Factory
{
    public class DestroySystem : IEcsPreInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData;

        private GameObjectFactory _factory;

        private EcsFilter<DestroyPrefabComponent> _destroyFilter = null;

        public void PreInit()
        {
            _factory = _sceneData.factory;
        }

        public void Run()
        {
            if (!_destroyFilter.IsEmpty())
            {
                foreach (int index in _destroyFilter)
                {
                    ref var destroyInfo = ref _destroyFilter.Get1(index);
                    _factory.Reclaim(destroyInfo);
                }
            }
        }
    }
}
