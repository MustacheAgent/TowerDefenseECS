using Scenarios;
using System;
using Dictionaries;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class SceneData : MonoBehaviour
    {
        public Scenario scenario;
        public int currency;
        public int baseHealth;

        #region Dictionaries
        public TowerDictionary towerDictionary;
        #endregion
    }
}