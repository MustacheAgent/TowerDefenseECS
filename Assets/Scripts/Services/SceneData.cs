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
        public GameObjectFactory factory;
        public GameObject worldBottomLeft;
        // параметры сетки
        public int gridSizeX;
        public int gridSizeZ;
        public EcsEntity[] tiles;
        public int2 spawn, destination;

        public TowerType selectedTower;
        public TowerDictionary towerDictionary;
        public Scenario scenario;
    }
}