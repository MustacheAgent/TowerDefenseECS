using Enums;
using Factories;
using Leopotam.Ecs;
using Scenarios;
using System;
using Dictionaries;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    [Serializable]
    public class SceneData : MonoBehaviour
    {
        public GameObjectFactory gameObjectFactory;
        
        public Scenario scenario;
        public TowerType selectedTower;

        #region Dictionaries
        public TowerDictionary towerDictionary;
        public EnemyDictionary enemies;
        #endregion
    }
}