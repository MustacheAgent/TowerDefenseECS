using Dictionaries;
using System;
using Factories;
using UnityEngine;

namespace Services
{
    [Serializable]
    [CreateAssetMenu(menuName = "Scriptable Objects/Factory Data", fileName = "FactoryData")]
    public class FactoryData : ScriptableObject
    {
        public GameObject mortarShellPrefab;
        public GameObject rocketPrefab;

        public GameObjectFactory factory;
        
        public EnemyDictionary enemiesDictionary;
    }
}