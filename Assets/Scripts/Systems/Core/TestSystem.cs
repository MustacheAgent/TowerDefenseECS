using Components;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Components;
using Scripts;
using Services;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class TestSystem : IEcsRunSystem
    {
        private PlayerInputData _input = null;
        EcsFilter<EcsUiClickEvent> _clickEvents;

        public void Run()
        {
            foreach (var idx in _clickEvents)
            {
                ref EcsUiClickEvent data = ref _clickEvents.Get1(idx);
                Debug.Log("Im clicked!", data.Sender);
            }
        }
    }
}
