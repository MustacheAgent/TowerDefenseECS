using System;
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
        public Wave[] waves;

        public int WaveIndex
        {
            get => _waveIndex + 1;
        }
        
        public int SeqLength => waves[_waveIndex].SeqLength;

        private int _waveIndex;
        private float _timeScale;

        public void Init()
        {
            Debug.Assert(waves.Length > 0, "Empty scenario!");
            _waveIndex = 0;
            waves[_waveIndex].Init();
            _timeScale = 1f;
        }

        public bool Progress()
        {
            if (_waveIndex >= waves.Length)
			{
				return false;
			}
            
            float deltaTime = waves[_waveIndex].Progress(_timeScale * Time.deltaTime);
            while (deltaTime >= 0f)
            {
                if (++_waveIndex >= waves.Length)
                {
                    return false;
                }
                
                WorldHandler.GetWorld().NewEntity().Get<WaveCompletedEvent>() = new WaveCompletedEvent
                {
                    WaveNumber = _waveIndex + 1
                };
                
                waves[_waveIndex].Init();
                deltaTime = waves[_waveIndex].Progress(deltaTime);
            }
            return true;
        }
    }
}
