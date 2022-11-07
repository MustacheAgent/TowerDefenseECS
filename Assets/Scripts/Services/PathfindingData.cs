using Leopotam.Ecs;
using Unity.Mathematics;
using UnityEngine;

namespace Services
{
    public class PathfindingData : MonoBehaviour
    {
        // параметры сетки
        public int gridSizeX;
        public int gridSizeZ;
        public EcsEntity[] tiles;
        public int2 spawn, destination;
    }
}