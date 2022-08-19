﻿using Factories;
using Leopotam.Ecs;
using System;
using Unity.Collections;
using Unity.Mathematics;
using UnityEngine;

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
}
