using Dictionaries;
using Scenarios;
using System;
using UnityEngine;

namespace Services
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Static Data", fileName = "StaticData")]
    public class StaticData : ScriptableObject
    {
        public EnemyDictionary enemies;
        public GameObject enemyPrefab;
        public GameObject wallPrefab;
        public GameObject laserPrefab;
        public GameObject mortarPrefab;
        public GameObject projectilePrefab;
    }
}