using ECS.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace ECS.Systems.Core
{
    public class TimerSystem : IEcsRunSystem
    {
        private EcsFilter<TimerComponent> _timers;
        
        public void Run()
        {
            foreach (var timerIdx in _timers)
            {
                ref var timer = ref _timers.Get1(timerIdx);
                if (!timer.IsPaused) timer.Current += Time.deltaTime;

                if (!(timer.Current >= timer.Cooldown)) continue;

                timer.Callback?.Invoke();
                _timers.GetEntity(timerIdx).Del<TimerComponent>();
            }
        }
    }
}