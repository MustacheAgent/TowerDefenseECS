using Enums;
using Factories;
using Leopotam.Ecs;
using Scenarios;
using System;
using Unity.Mathematics;
using UnityEngine;

[Serializable]
public class SceneData : MonoBehaviour
{
    public GameObjectFactory factory;
    public GameObject worldBottomLeft;
    public TowerType selectedTower;

    // параметры сетки
    public int gridSizeX;
    public int gridSizeZ;

    public EcsEntity[] tiles;
    public int2 spawn, destination;

    public Scenario scenario;
}
