﻿using Components;
using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using Scripts;
using Events.Enemies;
using System;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, PathComponent, FindPathEvent> units = null;
        readonly EcsFilter<PositionComponent, DestinationTag> dest = null;
        readonly EcsFilter<PositionComponent, SpawnTag> spawn = null;
        private SceneData _sceneData = null;

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public void Init()
        {
            SetGridSize();
            SetInitialState();
            //SetPathfindingValues();
        }

        public void Run()
        {
            ref var pos = ref dest.Get1(0);
            foreach(var i in units)
            {
                units.GetEntity(i).Del<FindPathEvent>();
                ref var unitPosition = ref units.Get1(i);
                ref var unitPath = ref units.Get2(i);
                SetPathfindingValues();
                List<int2> path = FindPath(unitPosition.transform.position, pos.transform.position);
                unitPath.path = new List<int2>(path);
                unitPath.pathIndex = 0;
            }
        }

        private List<int2> FindPath(Vector3 start, Vector3 end)
        {
            int startNodeIndex = NodeFromPoint(start);
            int endNodeIndex = NodeFromPoint(end);

            EcsEntity startNode = _sceneData.tiles[startNodeIndex];
            EcsEntity endNode = _sceneData.tiles[endNodeIndex];
            ref var startPath = ref startNode.Get<PathfindingComponent>();
            ref var endPath = ref endNode.Get<PathfindingComponent>();
            startPath.gCost = 0;

            int2[] neighbours = new int2[]
            {
                new int2(-1, 0),
                new int2(+1, 0),
                new int2(0, -1),
                new int2(0, +1),
                new int2(-1, -1),
                new int2(-1, +1),
                new int2(+1, -1),
                new int2(+1, +1)
            };

            List<int> openList = new();
            List<int> closedList = new();

            openList.Add(startPath.index);

            while(openList.Count > 0)
            {
                int currentIndex = GetLowestFCost(openList);
                ref var currentPath = ref _sceneData.tiles[currentIndex].Get<PathfindingComponent>();

                if (currentIndex == endNodeIndex)
                {
                    // конец пути достигнут
                    break;
                }

                for (int i = 0; i < openList.Count; i++)
                {
                    if (openList[i] == currentIndex)
                    {
                        openList.Remove(currentIndex);
                        break;
                    }
                }

                closedList.Add(currentIndex);

                for (int i = 0; i < neighbours.Length; i++)
                {
                    int2 neighbourOffset = neighbours[i];
                    int2 neighbourPosition = new(currentPath.position.x + neighbourOffset.x, 
                        currentPath.position.y + neighbourOffset.y);

                    if (!IsPositionInsideGrid(neighbourPosition))
                    {
                        continue;
                    }

                    int neighbourIndex = PathfindingExtensions.CalculateIndex(neighbourPosition.x, neighbourPosition.y, _sceneData.gridSizeX);

                    if (closedList.Contains(neighbourIndex))
                    {
                        continue;
                    }

                    ref var neighbourPath = ref _sceneData.tiles[neighbourIndex].Get<PathfindingComponent>();
                    if (!neighbourPath.isWalkable)
                    {
                        continue;
                    }

                    int tentativeGCost = currentPath.gCost + CalculateDistanceCost(currentPath.position, neighbourPath.position);
                    if (tentativeGCost < neighbourPath.gCost)
                    {
                        neighbourPath.cameFromIndex = currentIndex;
                        neighbourPath.gCost = tentativeGCost;

                        if (!openList.Contains(neighbourIndex))
                        {
                            openList.Add(neighbourIndex);
                        }
                    }
                }
            }

            if (endPath.cameFromIndex == -1)
            {
                return null;
            }
            else
            {
                return CalculatePath(endPath);
            }
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

        private void SetPathfindingValues()
        {
            for (int i = 0; i < _sceneData.tiles.Length; i++)
            {
                ref var path = ref _sceneData.tiles[i].Get<PathfindingComponent>();
                path.cameFromIndex = -1;
                path.gCost = int.MaxValue;
                path.hCost = CalculateDistanceCost(path.position, _sceneData.destination);
            }
        }

        private void SetInitialState()
        {
            EcsEntity entity;
            RaycastHit hit;
            Vector3 first;
            Vector3 boxCastPos = first = _sceneData.worldBottomLeft.transform.position;
            GameObject obj = _sceneData.worldBottomLeft;

            _sceneData.tiles = new EcsEntity[_sceneData.gridSizeX * _sceneData.gridSizeZ];

            for (int x = 0; x < _sceneData.gridSizeX; x++)
            {
                entity = obj.GetEntity();
                ref var path = ref entity.Get<PathfindingComponent>();
                path.position.x = x;
                path.position.y = 0;
                path.index = PathfindingExtensions.CalculateIndex(x, 0, _sceneData.gridSizeX);

                if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == Enums.TileContent.SpawnPoint)
                {
                    _sceneData.spawn = new int2(path.position.x, path.position.y);
                }
                if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == Enums.TileContent.Destination)
                {
                    _sceneData.spawn = new int2(path.position.x, path.position.y);
                }

                ref var position = ref entity.Get<PositionComponent>();
                boxCastPos = first = position.transform.position;

                _sceneData.tiles[path.index] = entity;

                for (int z = 1; z < _sceneData.gridSizeZ; z++)
                {
                    if (Physics.BoxCast(boxCastPos, new Vector3(0.1f, 0.1f, 500), Vector3.forward, out hit,
                        _sceneData.worldBottomLeft.transform.rotation))
                    {
                        entity = hit.transform.gameObject.GetEntity();
                        path = ref entity.Get<PathfindingComponent>();
                        path.position.x = x;
                        path.position.y = z;
                        path.index = PathfindingExtensions.CalculateIndex(x, z, _sceneData.gridSizeX);

                        _sceneData.tiles[path.index] = entity;

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
        }

        private List<int2> CalculatePath(PathfindingComponent endPath)
        {
            if (endPath.cameFromIndex == -1)
            {
                return null;
            }
            List<int2> path = new();
            ref var currentPath = ref _sceneData.tiles[endPath.index].Get<PathfindingComponent>();
            
            while (currentPath.cameFromIndex != -1)
            {
                ref var cameFromPath = ref _sceneData.tiles[currentPath.cameFromIndex].Get<PathfindingComponent>();
                path.Add(cameFromPath.position);
                currentPath = cameFromPath;
            }
            path.Reverse();

            return path;
        }

        private bool IsPositionInsideGrid(int2 position)
        {
            return 
                position.x >= 0 &&
                position.y >= 0 &&
                position.x < _sceneData.gridSizeX && 
                position.y < _sceneData.gridSizeZ;
        }

        private int GetLowestFCost(List<int> openList)
        {
            ref var lowestPath = ref _sceneData.tiles[openList[0]].Get<PathfindingComponent>();
            int index = lowestPath.index;

            for (int i = 1; i < openList.Count; i++)
            {
                ref var currentPath = ref _sceneData.tiles[openList[i]].Get<PathfindingComponent>();
                if (currentPath.fCost < lowestPath.fCost)
                {
                    index = currentPath.index;
                }
            }

            return index;
        }

        private int NodeFromPoint(Vector3 point)
        {
            int x = Mathf.RoundToInt(point.x);
            int y = Mathf.RoundToInt(point.z);
            //Debug.Log("Point: " + point + "\n" + "Got indexes. X: " + x + " Z: " + y);
            if (x >= 0 && x < _sceneData.gridSizeX && y >= 0 && y < _sceneData.gridSizeZ)
            {
                int index = x + y * _sceneData.gridSizeX;
                //Debug.Log("Index: " + index);
                return index;
            }
            return -1;
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
