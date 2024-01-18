using System.Linq;
using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
using Services;
using UnityEngine;

namespace ECS.Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private BreadthFirstSearch _bfs;
        private Tile[] _tiles;
        private Tile[] _destinations;
        
        private readonly EcsWorld _world = null;

        private readonly GridData _gridData = null;

        private readonly EcsFilter<FindPathEvent> _findPathFilter = null;
        
        public void Init()
        {
            _tiles = GameObject.FindObjectsOfType<Tile>();
            
            foreach (var tile in _tiles)
            {
                tile.InvertNeighbors();
            }
            
            _bfs = new BreadthFirstSearch();
            _world.NewEntity().Get<FindPathEvent>();
        }
        
        public void Run()
        {
            foreach (var i in _findPathFilter)
            {
                _findPathFilter.GetEntity(i).Del<FindPathEvent>();
               
                ResetPath();

                _bfs.FindPath(_gridData.destinationTiles);

                foreach (var tile in _tiles)
                {
                    if (tile.hasPath) continue;
                    
                    ResetPath();
                    _bfs.FindPath(_gridData.destinationTiles, true);
                    break;
                }
                
                HidePath();
            }
        }

        private void ResetPath()
        {
            foreach (var tile in _tiles)
            {
                tile.Reset();
            }
        }

        private void ShowPath()
        {
            foreach (var tile in _tiles)
            {
                tile.ShowPath();
            }
        }
        
        private void HidePath()
        {
            foreach (var tile in _tiles)
            {
                tile.HidePath();
            }
        }
    }
}