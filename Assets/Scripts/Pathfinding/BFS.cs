using System.Collections.Generic;

namespace Pathfinding
{
    public class BreadthFirstSearch
    {
        private Queue<Tile> _searchFrontier;
        
        public bool FindPath(Tile[] destinations, bool ignoreWalkable = false)
        {
            _searchFrontier = new Queue<Tile>();

            for (var i = 0; i < destinations.Length; i++)
            {
                _searchFrontier.Enqueue(destinations[i]);
            }

            if (_searchFrontier.Count == 0)
            {
                return false;
            }

            while (_searchFrontier.Count > 0)
            {
                var tile = _searchFrontier.Dequeue();
                if (tile != null)
                {
                    tile.InvertNeighbors();

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
                    
                    tile.processed = true;
                    tile.ShowPath();
                }
            }

            return true;
        }

        private Tile GrowPathTo(Tile tile, Tile neighbor, bool ignoreWalkable)
        {
            if (neighbor == null || neighbor.processed)
                return null;

            if (!ignoreWalkable)
            {
                if (!neighbor.walkable)
                    return null;
            }
            
            neighbor.distance = tile.distance + 1;
            neighbor.next = tile;
            neighbor.processed = true;
            
            return neighbor;
        }
    }
}