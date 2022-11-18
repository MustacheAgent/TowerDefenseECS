using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using MonoProviders.Components.Towers;
using Services;
using UnityComponents.UI;

namespace Systems.UI
{
    public class TowerButtonClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<EcsUiClickEvent> _clickEvents;
        readonly SceneData _sceneData = null;

        public void Init()
        {
            foreach (var tower in _sceneData.towerDictionary)
            {
                
            }
        }
        
        public void Run()
        {
            foreach (var idx in _clickEvents)
            {
                ref EcsUiClickEvent data = ref _clickEvents.Get1(idx);
                var type = data.Sender.GetComponentInParent<BuildTowerButton>().towerType;
                if (_sceneData.towerDictionary[type].GetComponent<TowerInfoProvider>().Value.towerPrice <=
                    _sceneData.currency)
                    _sceneData.selectedTower = type;
            }
        }
    }
}
