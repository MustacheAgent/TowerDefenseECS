using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Scenario", fileName = "LevelScenario")]
    public class Scenario : ScriptableObject
    {
        public Wave[] waves;
    }
}
