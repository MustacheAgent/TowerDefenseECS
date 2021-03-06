using System;
using UnityEngine;

[Serializable]
[CreateAssetMenu(menuName = "Scriptable Objects/Scene Data", fileName = "StaticData")]
public class StaticData : ScriptableObject
{
    public GameObject enemyPrefab;
}