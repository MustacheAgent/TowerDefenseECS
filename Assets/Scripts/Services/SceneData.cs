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
        public UIObjectFactory uiFactory;
        
        public GameObject worldBottomLeft;
        public Canvas hud;
        public Scenario scenario;
        public TowerType selectedTower;
        public TowerDictionary towerDictionary;
    }
}