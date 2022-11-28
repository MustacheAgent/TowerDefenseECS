using Components;
using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;
using Unity.Mathematics;
using System.Collections.Generic;
using Enums;
using Scripts;
using Events.Enemies;
using Services;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<PositionComponent, PathComponent, FindPathEvent> _units = null;
        private readonly EcsFilter<PositionComponent, DestinationTag> _dest = null;
        private readonly PathfindingData _pathfindingData = null;

        private readonly EcsWorld _world = null;

        private const int MOVE_STRAIGHT_COST = 10;
        private const int MOVE_DIAGONAL_COST = 14;

        public void Init()
        {
            //SetGridSize();
            CreateGrid();
            //SetInitialState();
            //SetPathfindingValues(_sceneData.tiles);
        }

        public void Run()
        {
            ref var pos = ref _dest.Get1(0);
            foreach(var i in _units)
            {
                _units.GetEntity(i).Del<FindPathEvent>();
                ref var unitPosition = ref _units.Get1(i);
                ref var unitPath = ref _units.Get2(i);
                List<int2> path = FindPath(unitPosition.transform.position, pos.transform.position);
                if (path != null)
                {
                    unitPath.path = new List<int2>(path);
                    unitPath.pathIndex = 1;
                    var currentPathXY = unitPath.path[unitPath.pathIndex];
                    var index = 
                        PathfindingExtensions.CalculateIndex(currentPathXY.x, currentPathXY.y, _pathfindingData.gridSizeX);
                    unitPath.currentDestination =
                        _pathfindingData.Tiles[index].Get<PositionComponent>().transform.position;
                }
            }
        }

        private List<int2> FindPath(Vector3 start, Vector3 end)
        {
            int startNodeIndex = NodeFromPoint(start);
            int endNodeIndex = NodeFromPoint(end);

            EcsEntity[] tiles = new EcsEntity[_pathfindingData.gridSizeX * _pathfindingData.gridSizeZ];
            _pathfindingData.Tiles.CopyTo(tiles, 0);
            SetPathfindingValues(tiles);

            EcsEntity startNode = tiles[startNodeIndex];
            EcsEntity endNode = tiles[endNodeIndex];
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
            
            /*
            int2[] neighbours = new int2[]
            {
                new int2(-1, 0),
                new int2(+1, 0),
                new int2(0, -1),
                new int2(0, +1)
            };
            */

            List<int> openList = new();
            List<int> closedList = new();

            openList.Add(startPath.index);

            while(openList.Count > 0)
            {
                int currentIndex = GetLowestFCost(openList, tiles);
                ref var currentPath = ref tiles[currentIndex].Get<PathfindingComponent>();

                if (currentIndex == endNodeIndex)
                {
                    // конец пути достигнут
                    break;
                }

                openList.Remove(currentIndex);

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

                    int neighbourIndex = PathfindingExtensions.CalculateIndex(neighbourPosition.x, neighbourPosition.y, _pathfindingData.gridSizeX);

                    if (closedList.Contains(neighbourIndex))
                    {
                        continue;
                    }

                    ref var neighbourPath = ref tiles[neighbourIndex].Get<PathfindingComponent>();
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
                return CalculatePath(endPath, tiles);
            }
        }

        /// <summary>
        /// Вычисляет размер сетки игрового поля.
        /// </summary>
        private void SetGridSize()
        {
            int gridSizeZ = 1;
            int gridSizeX = 1;
            Vector3 raycastPos = _pathfindingData.worldBottomLeft.transform.position;

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

            _pathfindingData.gridSizeX = gridSizeX;
            _pathfindingData.gridSizeZ = gridSizeZ;
        }

        private void SetPathfindingValues(EcsEntity[] tiles)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                ref var path = ref tiles[i].Get<PathfindingComponent>();
                path.cameFromIndex = -1;
                path.gCost = int.MaxValue;
                path.hCost = CalculateDistanceCost(path.position, _pathfindingData.Destination);
            }
        }

        private void CreateGrid()
        {
            GameObject obj = _pathfindingData.worldBottomLeft;

            Vector3 first = obj.transform.position;
            _pathfindingData.Tiles = new EcsEntity[_pathfindingData.gridSizeX * _pathfindingData.gridSizeZ];

            for (var x = 0; x < _pathfindingData.gridSizeX; x++)
            {
                if (obj == null)
                {
                    var entity = _world.NewEntity();
                    ref var path = ref entity.Get<PathfindingComponent>();
                    path.position.x = x;
                    path.position.y = 0;
                    path.index = PathfindingExtensions.CalculateIndex(x, 0, _pathfindingData.gridSizeX);
                    path.isWalkable = false;
                    entity.Get<TileContentComponent>().content = TileContent.NonBuildable;
                    _pathfindingData.Tiles[path.index] = entity;
                }
                else
                {
                    var entity = obj.GetEntity();
                    ref var path = ref entity.Get<PathfindingComponent>();
                    path.position.x = x;
                    path.position.y = 0;
                    path.index = PathfindingExtensions.CalculateIndex(x, 0, _pathfindingData.gridSizeX);

                    if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == TileContent.SpawnPoint)
                    {
                        _pathfindingData.Spawn = new int2(path.position.x, path.position.y);
                    }
                    if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == TileContent.Destination)
                    {
                        _pathfindingData.Destination = new int2(path.position.x, path.position.y);
                    }

                    _pathfindingData.Tiles[path.index] = entity;
                }

                var boxCastPos = first;
                RaycastHit hit;
                
                for (var z = 1; z < _pathfindingData.gridSizeZ; z++)
                {
                    if (Physics.BoxCast(boxCastPos, new Vector3(0.1f, 0.1f, 0.1f), Vector3.forward, out hit,
                            _pathfindingData.worldBottomLeft.transform.rotation, 1))
                    {
                        var entity = hit.transform.gameObject.GetEntity();
                        ref var path = ref entity.Get<PathfindingComponent>();
                        path.position.x = x;
                        path.position.y = z;
                        path.index = PathfindingExtensions.CalculateIndex(x, z, _pathfindingData.gridSizeX);
                        _pathfindingData.Tiles[path.index] = entity;
                    }
                    else
                    {
                        var entity = _world.NewEntity();
                        ref var path = ref entity.Get<PathfindingComponent>();
                        path.position.x = x;
                        path.position.y = z;
                        path.index = PathfindingExtensions.CalculateIndex(x, z, _pathfindingData.gridSizeX);
                        path.isWalkable = false;
                        entity.Get<TileContentComponent>().content = TileContent.NonBuildable;
                        _pathfindingData.Tiles[path.index] = entity;
                    }
                    
                    boxCastPos += Vector3.forward;
                }

                if (x <= _pathfindingData.gridSizeX - 1)
                {
                    if (Physics.BoxCast(first, new Vector3(0.1f, 0.1f, 0.1f), Vector3.right, out hit,
                            _pathfindingData.worldBottomLeft.transform.rotation, 1))
                    {
                        obj = hit.transform.gameObject;
                        first = obj.transform.position;
                    }
                    else
                    {
                        obj = null;
                        first += Vector3.right;
                    }                    
                }
            }
        }

        private void SetInitialState()
        {
            GameObject obj = _pathfindingData.worldBottomLeft;

            _pathfindingData.Tiles = new EcsEntity[_pathfindingData.gridSizeX * _pathfindingData.gridSizeZ];

            for (int x = 0; x < _pathfindingData.gridSizeX; x++)
            {
                var entity = obj.GetEntity();
                ref var path = ref entity.Get<PathfindingComponent>();
                path.position.x = x;
                path.position.y = 0;
                path.index = PathfindingExtensions.CalculateIndex(x, 0, _pathfindingData.gridSizeX);

                if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == Enums.TileContent.SpawnPoint)
                {
                    _pathfindingData.Spawn = new int2(path.position.x, path.position.y);
                }
                if (entity.Has<TileContentComponent>() && entity.Get<TileContentComponent>().content == Enums.TileContent.Destination)
                {
                    _pathfindingData.Destination = new int2(path.position.x, path.position.y);
                }

                ref var position = ref entity.Get<PositionComponent>();
                Vector3 first;
                var boxCastPos = first = position.transform.position;

                _pathfindingData.Tiles[path.index] = entity;

                RaycastHit hit;
                for (int z = 1; z < _pathfindingData.gridSizeZ; z++)
                {
                    if (Physics.BoxCast(boxCastPos, new Vector3(0.1f, 0.1f, 500), Vector3.forward, out hit,
                            _pathfindingData.worldBottomLeft.transform.rotation))
                    {
                        entity = hit.transform.gameObject.GetEntity();
                        path = ref entity.Get<PathfindingComponent>();
                        path.position.x = x;
                        path.position.y = z;
                        path.index = PathfindingExtensions.CalculateIndex(x, z, _pathfindingData.gridSizeX);

                        _pathfindingData.Tiles[path.index] = entity;

                        //position = ref entity.Get<PositionComponent>();
                        //boxCastPos = position.transform.position;
                    }
                    boxCastPos += Vector3.forward;
                }

                if (Physics.BoxCast(first, new Vector3(0.1f, 0.1f, 500), Vector3.right, out hit,
                        _pathfindingData.worldBottomLeft.transform.rotation))
                {
                    obj = hit.transform.gameObject;
                }
            }
        }
        

        private List<int2> CalculatePath(PathfindingComponent endPath, EcsEntity[] tiles)
        {
            if (endPath.cameFromIndex == -1)
            {
                return null;
            }
            List<int2> path = new();
            ref var currentPath = ref tiles[endPath.index].Get<PathfindingComponent>();
            path.Add(currentPath.position);
            
            while (currentPath.cameFromIndex != -1)
            {
                ref var cameFromPath = ref tiles[currentPath.cameFromIndex].Get<PathfindingComponent>();
                path.Add(cameFromPath.position);
                currentPath = ref tiles[currentPath.cameFromIndex].Get<PathfindingComponent>();
            }
            path.Reverse();

            return path;
        }

        private bool IsPositionInsideGrid(int2 position)
        {
            return 
                position.x >= 0 &&
                position.y >= 0 &&
                position.x < _pathfindingData.gridSizeX && 
                position.y < _pathfindingData.gridSizeZ;
        }

        private int GetLowestFCost(List<int> openList, EcsEntity[] tiles)
        {
            ref var lowestPath = ref tiles[openList[0]].Get<PathfindingComponent>();
            int index = lowestPath.index;

            for (int i = 1; i < openList.Count; i++)
            {
                ref var currentPath = ref tiles[openList[i]].Get<PathfindingComponent>();
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
            if (x >= 0 && x < _pathfindingData.gridSizeX && y >= 0 && y < _pathfindingData.gridSizeZ)
            {
                int index = x + y * _pathfindingData.gridSizeX;
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
