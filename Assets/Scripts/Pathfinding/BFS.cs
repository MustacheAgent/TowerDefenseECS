using System.Collections.Generic;

namespace Pathfinding
{
    public class BreadthFirstSearch
    {
        private Queue<Tile> _searchFrontier;
        
        public void FindPath(Tile[] destinations)
        {
            _searchFrontier = new Queue<Tile>();

            for (var i = 0; i < destinations.Length; i++)
            {
                _searchFrontier.Enqueue(destinations[i]);
            }

            while (_searchFrontier.Count > 0)
            {
                var tile = _searchFrontier.Dequeue();
                if (tile != null)
                {
                    tile.InvertNeighbors();

                    if (tile.alternative)
                    {
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.north));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.south));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.east));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.west));
                    }
                    else
                    {
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.west));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.east));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.south));
                        _searchFrontier.Enqueue(GrowPathTo(tile, tile.north));
                    }
                    
                    tile.processed = true;
                    tile.ShowPath();
                }
            }
        }

        private Tile GrowPathTo(Tile tile, Tile neighbor)
        {
            if (neighbor == null || neighbor.processed)
                return null;
            
            neighbor.distance = tile.distance + 1;
            neighbor.next = tile;
            neighbor.processed = true;
            
            return neighbor;
        }
    }
}