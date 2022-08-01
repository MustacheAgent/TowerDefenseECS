using Factories;
using Leopotam.Ecs;
using System;
using UnityEngine;

[Serializable]
public class SceneData : MonoBehaviour
{
    public EnemyFactory enemyFactory;
    public GameObject worldBottomLeft;

    // параметры сетки
    public int gridSizeX;
    public int gridSizeZ;

    public EcsEntity[,] tiles;
}
