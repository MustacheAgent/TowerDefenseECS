using System.Collections.Generic;
using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
using Services;
using UnityEditor.Rendering.Universal.ShaderGUI;
using UnityEngine;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private BreadthFirstSearch _bfs;
        private Tile[] _tiles;
        
        private readonly EcsWorld _world = null;

        private readonly GridData _gridData = null;

        private readonly EcsFilter<FindPathEvent> _findPathFilter = null;
        
        public void Init()
        {
            _tiles = GameObject.FindObjectsOfType<Tile>();
            _bfs = new BreadthFirstSearch();
            _world.NewEntity().Get<FindPathEvent>();
        }
        
        public void Run()
        {
            foreach (var i in _findPathFilter)
            {
                _findPathFilter.GetEntity(i).Del<FindPathEvent>();

                foreach (var tile in _tiles)
                {
                    tile.Reset();
                }
                
                if (!_bfs.FindPath(_gridData.destinationTiles))
                {
                    _bfs.FindPath(_gridData.destinationTiles, true);
                }
            }
        }
    }
}