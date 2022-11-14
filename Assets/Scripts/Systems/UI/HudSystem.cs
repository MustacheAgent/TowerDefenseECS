using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Services;
using UnityComponents.UI;

namespace Systems.UI
{
    public class HudSystem : IEcsInitSystem, IEcsRunSystem
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
                _sceneData.selectedTower = data.Sender.GetComponentInParent<BuildTowerButton>().towerType;
            }
        }
    }
}
