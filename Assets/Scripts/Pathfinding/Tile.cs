using UnityEngine;

namespace Pathfinding
{
    public class Tile : MonoBehaviour
    {
        public Transform arrow;
        
        [Header("Breadth-First Search")]
        [HideInInspector] public bool processed;
        public bool walkable = true;
        public bool visited = false;
        //public List<Tile> neighbourTiles;
        //[HideInInspector] 
        public Tile north, west, east, south, next;
        public int distance;

        private void Start()
        {
            GetNeighbors();
        }

        public void Reset()
        {
            visited = false;
            next = null;
            distance = 0;
        }

        private void GetNeighbors()
        {
            Reset();
            north = west = east = south = null;
            
            north = CheckTile(Vector3.forward);
            south = CheckTile(-Vector3.forward);
            east = CheckTile(Vector3.right);
            west = CheckTile(-Vector3.right);
        }

        private Tile CheckTile(Vector3 direction)
        {
            var halfExtents = new Vector3(0.25f, 0.5f, 0.25f);
            var colliders = Physics.OverlapBox(transform.position + direction, halfExtents);

            foreach (var item in colliders)
            {
                var tile = item.GetComponent<Tile>();
                //var tile = item.GetComponent<Tile>();
                if (tile != null)
                {
                    return tile;
                }
            }

            return null;
        }
    }
}
