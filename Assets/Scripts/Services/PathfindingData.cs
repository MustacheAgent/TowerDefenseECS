using System;
using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    public class PathfindingData : MonoBehaviour
    {
        // параметры сетки
        public GameObject worldBottomLeft;
        [NonSerialized] public int gridSizeX;
        [NonSerialized] public int gridSizeZ;
        [NonSerialized] public EcsEntity[] tiles;
        [NonSerialized] public int2 spawn, destination;
    }
}