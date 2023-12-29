using System.Collections.Generic;
using UnityEngine;

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
                if (tile != null)
                {
                    tile.InvertNeighbors();

                    switch (tile.walkable)
                    {
                        case true when tile.alternative:
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.north, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.south, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.east, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.west, ignoreWalkable));
                            break;
                        case true:
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.west, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.east, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.south, ignoreWalkable));
                            _searchFrontier.Enqueue(GrowPathTo(tile, tile.north, ignoreWalkable));
                            break;
                        case false:
                            Debug.Log("found unwalkable tile!" + tile);
                            break;
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

            if (ignoreWalkable == false)
            {
                if (neighbor.walkable == false)
                    return null;
            }
            
            neighbor.distance = tile.distance + 1;
            neighbor.next = tile;
            neighbor.processed = true;
            
            return neighbor;
        }

    }
}