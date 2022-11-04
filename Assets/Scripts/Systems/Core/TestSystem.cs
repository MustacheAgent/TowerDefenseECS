using Components;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Scripts;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class TestSystem : IEcsRunSystem
    {
        private PlayerInputData _input = null;
        private SceneData _sceneData = null;
        EcsFilter<EcsUiClickEvent> _clickEvents;

        public void Run()
        {
            foreach (var idx in _clickEvents)
            {
                ref EcsUiClickEvent data = ref _clickEvents.Get1(idx);
                Debug.Log("Im clicked!", data.Sender);
            }
        }

        private int NodeFromPoint(Vector3 point)
        {
            int x = Mathf.RoundToInt(point.x);
            int y = Mathf.RoundToInt(point.z);
            Debug.Log("Point: " + point + "\n" + "Got indexes. X: " + x + " Z: " + y);
            if (x >= 0 && x < _sceneData.gridSizeX && y >= 0 && y < _sceneData.gridSizeZ)
            {
                return x + y * _sceneData.gridSizeX;
            }
            return -1;
        }
    }
}
