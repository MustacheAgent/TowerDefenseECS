using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenario", fileName = "LevelScenario")]
    public class Scenario : ScriptableObject
    {
        public Wave[] waves;
        public float timeSpan;

        private int _index;
        private float _timeScale;
        private float _currentTime;

        public void Init()
        {
            Debug.Assert(waves.Length > 0, "Empty scenario!");
            _index = 0;
            waves[_index].Init();
            _timeScale = 1f;
        }

        public bool Progress()
        {
            if (_index >= waves.Length)
			{
				return false;
			}
            
            float deltaTime = waves[_index].Progress(_timeScale * Time.deltaTime);
            while (deltaTime >= 0f)
            {
                if (++_index >= waves.Length)
                {
                    return false;
                }
                waves[_index].Init();
                deltaTime = waves[_index].Progress(deltaTime);
            }
            return true;
        }
    }
}
