using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Level List", fileName = "LevelList")]
    public class LevelList : ScriptableObject
    {
        public Scenario[] levels;
    }
}