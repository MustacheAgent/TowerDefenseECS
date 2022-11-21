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
        private readonly EcsFilter<PlayerHealthChangedEvent> _healthChanged = null;
        
        public void Init()
        {
            _hudData.currency.text = _sceneData.currency.ToString();
            _hudData.baseHealth.text = _sceneData.baseHealth.ToString();
        }

        public void Run()
        {
            foreach (var eventIndex in _currencyChanged)
            {
                ref var newCurrency = ref _currencyChanged.Get1(eventIndex).CurrencyChanged;
                _sceneData.currency += newCurrency;
                _hudData.currency.text = _sceneData.currency.ToString();
            }
            
            foreach (var eventIndex in _healthChanged)
            {
                ref var newHealth = ref _healthChanged.Get1(eventIndex).HealthChanged;
                _sceneData.baseHealth += newHealth;
                _hudData.baseHealth.text = _sceneData.baseHealth.ToString();
            }
        }
    }
}