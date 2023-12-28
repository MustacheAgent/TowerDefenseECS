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
        public int gridSizeX;
        public int gridSizeZ;
        [NonSerialized] public EcsEntity[] Tiles;
        [NonSerialized] public int2 Spawn, Destination;
    }
}