using Events;
using Leopotam.Ecs;
using Services;
using Voody.UniLeo;

namespace Systems.UI
{
    public class HudUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly HudData _hudData = null;

        private readonly EcsFilter<CurrencyChangedEvent> _currencyChanged = null;
        
        public void Init()
        {
            _hudData.currency.text = _sceneData.currency.ToString();
        }

        public void Run()
        {
            foreach (var eventIndex in _currencyChanged)
            {
                ref var newCurrency = ref _currencyChanged.Get1(eventIndex).CurrencyChange;
                _sceneData.currency += newCurrency;
                _hudData.currency.text = _sceneData.currency.ToString();
                //_currencyChanged.GetEntity(eventIndex).Del<CurrencyChangedEvent>();
            }
        }
    }
}