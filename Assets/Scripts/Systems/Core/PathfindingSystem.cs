using Assets.Scripts;
using Components;
using Components.Core;
using Leopotam.Ecs;
using Tags;
using Unity.Collections;
using UnityEngine;
using Unity.Mathematics;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, FindPathTag> filter = null;
        private SceneData _sceneData;
        private EcsEntity[,] tiles;

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public void Init()
        {
            SetGridSize();
            SetInitialState();
        }        

        public void Run()
        {
            foreach(var i in filter)
            {

            }
        }

        private void FindPath(Vector3 start, Vector3 end)
        {

        }

        /// <summary>
        /// Вычисляет размер сетки игрового поля.
        /// </summary>
        private void SetGridSize()
        {
            int gridSizeZ = 1;
            int gridSizeX = 1;
            Vector3 raycastPos = _sceneData.worldBottomLeft.transform.position;

            // получение размера сетки по Z
            while (true)
            {
                // TODO: возможно надо увеличить расстояние луча (ведь клетки не всегда будут стоят вплотную)
                if (Physics.Raycast(raycastPos, Vector3.forward, out RaycastHit ray, 1))
                {
                    raycastPos = ray.collider.transform.position;
                    gridSizeZ++;
                }
                else
                {
                    break;
                }
            }

            // получение размера сетки по Х
            while (true)
            {
                if (Physics.Raycast(raycastPos, Vector3.right, out RaycastHit ray, 1))
                {
                    raycastPos = ray.collider.transform.position;
                    gridSizeX++;
                }
                else
                {
                    break;
                }
            }

            _sceneData.gridSizeX = gridSizeX;
            _sceneData.gridSizeZ = gridSizeZ;
        }

        private void SetInitialState()
        {
            EcsEntity entity;
            RaycastHit hit;
            Vector3 first;
            Vector3 boxCastPos = first = _sceneData.worldBottomLeft.transform.position;
            GameObject obj = _sceneData.worldBottomLeft;

            tiles = new EcsEntity[_sceneData.gridSizeX, _sceneData.gridSizeZ];

            for (int x = 0; x < _sceneData.gridSizeX; x++)
            {
                entity = obj.GetEntity();
                ref var path = ref entity.Get<PathfindingComponent>();
                path.x = x;
                path.z = 0;
                path.index = CalculateIndex(x, 0, _sceneData.gridSizeX);
                path.IsWalkable = true;

                ref var position = ref entity.Get<PositionComponent>();
                boxCastPos = first = position.transform.position;

                tiles[x, 0] = entity;

                for (int z = 1; z < _sceneData.gridSizeZ; z++)
                {
                    if (Physics.BoxCast(boxCastPos, new Vector3(0.1f, 0.1f, 500), Vector3.forward, out hit,
                        _sceneData.worldBottomLeft.transform.rotation))
                    {
                        entity = hit.transform.gameObject.GetEntity();
                        path = ref entity.Get<PathfindingComponent>();
                        path.x = x;
                        path.z = z;
                        path.index = CalculateIndex(x, z, _sceneData.gridSizeX);
                        path.IsWalkable = true;
                        path.cameFromIndex = -1;

                        tiles[x, z] = entity;

                        position = ref entity.Get<PositionComponent>();
                        boxCastPos = position.transform.position;
                    }
                }

                if (Physics.BoxCast(first, new Vector3(0.1f, 0.1f, 500), Vector3.right, out hit,
                        _sceneData.worldBottomLeft.transform.rotation))
                {
                    obj = hit.transform.gameObject;
                }
            }

            _sceneData.tiles = tiles;
        }

        private int CalculateIndex(int x, int y, int width)
        {
            return x + y * width;
        }

        private int CalculateDistanceCost(int2 aPos, int2 bPos)
        {
            int xDistance = math.abs(aPos.x - bPos.x);
            int zDistance = math.abs(aPos.y - bPos.y);
            int remaining = math.abs(xDistance - zDistance);
            return MOVE_DIAGONAL_COST * math.min(xDistance, zDistance) + MOVE_STRAIGHT_COST * remaining;
        }
    }
}
