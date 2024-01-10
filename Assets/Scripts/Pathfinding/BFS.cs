using System.Collections.Generic;

namespace Pathfinding
{
    public class BreadthFirstSearch
    {
        private Queue<Tile> _searchFrontier = new();
        
        public bool FindPath(Tile[] destinations, bool ignoreWalkable = false)
        {
            _searchFrontier.Clear();
            
            for (var i = 0; i < destinations.Length; i++)
            {
                _searchFrontier.Enqueue(destinations[i]);
            }

            if (_searchFrontier.Count == 0) return false;

            while (_searchFrontier.Count > 0)
            {
                var tile = _searchFrontier.Dequeue();
                
                if (tile == null) continue;
                
                if (tile.alternative)
                {
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.north, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.south, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.east, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.west, ignoreWalkable));
                }
                else
                {
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.west, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.east, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.south, ignoreWalkable));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.north, ignoreWalkable));
                }
            }

            return true;
        }

        private Tile GrowPathTo(Tile tile, Tile neighbor, bool ignoreWalkable)
        {
            if (!tile.hasPath || neighbor == null || neighbor.hasPath)
                return null;
            
            neighbor.distance = tile.distance + 1;
            neighbor.next = tile;

            if (!ignoreWalkable) 
                return neighbor.walkable ? neighbor : null;
            
            return neighbor;
        }

    }
}