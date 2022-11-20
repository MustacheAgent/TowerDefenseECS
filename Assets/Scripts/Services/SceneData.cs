using Enums;
using Factories;
using Scenarios;
using System;
using Dictionaries;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class SceneData : MonoBehaviour
    {
        public GameObjectFactory gameObjectFactory;
        
        public Scenario scenario;
        public TowerType selectedTower;
        public int currency;
        public int baseHealth;

        #region Dictionaries
        public TowerDictionary towerDictionary;
        public EnemyDictionary enemiesDictionary;
        #endregion
    }
}