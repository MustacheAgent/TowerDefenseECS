using System.Collections.Generic;

namespace Pathfinding
{
    public class BreadthFirstSearch
    {
        private Queue<Tile> _searchFrontier;
        
        public void FindPath(Tile destination)
        {
            _searchFrontier = new Queue<Tile>();
            
            _searchFrontier.Enqueue(destination);

            while (_searchFrontier.Count > 0)
            {
                var tile = _searchFrontier.Dequeue();
                if (tile != null)
                {
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.north));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.east));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.south));
                    _searchFrontier.Enqueue(GrowPathTo(tile, tile.west));
                    tile.processed = true;
                    
                    tile.ShowPath();
                }
            }
        }

        private Tile GrowPathTo(Tile tile, Tile neighbor)
        {
            //if (!tile.processed || neighbor == null || neighbor.processed)
            if (neighbor == null || neighbor.processed)
                return null;
            
            neighbor.distance = tile.distance + 1;
            neighbor.next = tile;
            neighbor.processed = true;
            
            return neighbor;
        }
    }
}