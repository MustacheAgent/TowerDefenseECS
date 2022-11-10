using Enums;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using MonoProviders.Components.Towers;
using Services;
using UnityEngine;
using UnityEngine.UIElements;

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
                Debug.Log("Im clicked!", data.Sender);

                _sceneData.selectedTower = data.Sender.GetComponentInParent<BuildTowerButton>().towerType;
            }
        }
    }
}
