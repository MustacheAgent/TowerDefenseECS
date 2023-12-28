using Enums;
using System;
using UnityEngine;

namespace Dictionaries
{
    [Serializable]
    public class EnemyDictionary : SerializableDictionary<EnemyType, GameObject> { }
    [Serializable]
    public class TowerDictionary : SerializableDictionary<TowerType, GameObject> { }
}
