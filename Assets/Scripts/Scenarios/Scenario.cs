using System;
using Components;
using Events.Scenario;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Scenarios
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenario", fileName = "LevelScenario")]
    public class Scenario : ScriptableObject
    {
        public string levelName;
        public string levelDescription;
        public string sceneName;
        
        public Wave[] waves;

        public int WaveIndex
        {
            get => _waveIndex + 1;
        }
        
        public int SeqLength => waves[_waveIndex].SeqLength;

        private int _waveIndex;
        private float _timeScale;

        private bool _isEventSent;

        public void Init()
        {
            Debug.Assert(waves.Length > 0, "Empty scenario!");
            _waveIndex = 0;
            _timeScale = 1f;

            _isEventSent = false;
        }

        public void NextWave()
        {
            if (_waveIndex >= waves.Length)
            {
                // можно добавить событие завершения сценария
                return;
            }
            waves[_waveIndex].Start();
            WorldHandler.GetWorld().NewEntity().Get<WaveStartedEvent>() = new WaveStartedEvent
            {
                WaveNumber = _waveIndex + 1
            };
        }

        public void PrepareNextWave()
        {
            _waveIndex++;
            if (_waveIndex >= waves.Length)
            {
                // можно добавить событие завершения сценария
                if (_isEventSent) return;
                _isEventSent = !_isEventSent;
                WorldHandler.GetWorld().NewEntity().Get<ScenarioCompletedEvent>();
                return;
            }
            var timer = new TimerComponent
            {
                Cooldown = waves[_waveIndex].delayBeforeNextWave
            };
            timer.Callback += NextWave;
            WorldHandler.GetWorld().NewEntity().Get<TimerComponent>() = timer;
        }

        public bool Progress()
        {
            if (_waveIndex >= waves.Length) return false;
            
            float deltaTime = waves[_waveIndex].Progress(_timeScale * Time.deltaTime);
            while (deltaTime >= 0f)
            {
                if (++_waveIndex >= waves.Length) return false;
                
                waves[_waveIndex].Start();
                WorldHandler.GetWorld().NewEntity().Get<WaveStartedEvent>() = new WaveStartedEvent
                {
                    WaveNumber = _waveIndex + 1
                };
                deltaTime = waves[_waveIndex].Progress(deltaTime);
            }
            return true;
        }
    }
}
