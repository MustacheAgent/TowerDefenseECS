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
        public GameObject mortarShellPrefab;
        public GameObject rocketPrefab;
        
        public EnemyDictionary enemiesDictionary;
    }
}