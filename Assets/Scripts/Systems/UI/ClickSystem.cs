using Enums;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Services;
using UnityEngine;

namespace Systems.UI
{
    public class ClickSystem : IEcsRunSystem
    {
        readonly EcsFilter<EcsUiClickEvent> _clickEvents;
        readonly SceneData _sceneData = null;

        public void Run()
        {
            foreach (var idx in _clickEvents)
            {
                ref EcsUiClickEvent data = ref _clickEvents.Get1(idx);
                switch (data.WidgetName)
                {
                    case "Wall":
                        _sceneData.selectedTower = TowerType.Wall;
                        break;
                    case "Laser":
                        _sceneData.selectedTower = TowerType.Laser;
                        break;
                    case "Mortar":
                        _sceneData.selectedTower = TowerType.Mortar;
                        break;
                    default:
                        Debug.LogWarning("Unsupported tower type!");
                        break;
                }
            }
        }
    }
}
