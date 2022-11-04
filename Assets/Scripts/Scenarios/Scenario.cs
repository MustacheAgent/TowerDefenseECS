using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenario", fileName = "LevelScenario")]
    public class Scenario : ScriptableObject
    {
        public Wave[] waves;

        private int index;
        private float timeScale;

        public void Init()
        {
            Debug.Assert(waves.Length > 0, "Empty scenario!");
            index = 0;
            waves[index].Init();
            timeScale = 1f;
        }

        public bool Progress()
        {
            if (index >= waves.Length)
			{
				return false;
			}
            float deltaTime = waves[index].Progress(timeScale * Time.deltaTime);
            while (deltaTime >= 0f)
				{
					if (++index >= waves.Length)
					{
						return false;
					}
					waves[index].Init();
					deltaTime = waves[index].Progress(deltaTime);
				}
				return true;
        }
    }
}
