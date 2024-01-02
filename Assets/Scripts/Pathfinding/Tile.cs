using UnityEngine;

namespace Pathfinding
{
    public class Tile : MonoBehaviour
    {
        public Transform arrow;
        
        [Header("Breadth-First Search")]
        [HideInInspector] public bool processed;
        [HideInInspector] public bool alternative;
        public TileType type = TileType.Tile;

        [Header("Buildable/Walkable")]
        public bool walkable = true;
        public bool isBuildable = true;
        
        public Tile north, west, east, south, next;
        public int distance;
        public bool HasPath => distance != int.MaxValue;

        private void Awake()
        {
            if (type is TileType.Spawn or TileType.Destination)
                arrow.gameObject.SetActive(false);

            GetNeighbors();
        }

        public void Reset()
        {
            next = null;
            distance = type == TileType.Destination ? 0 : int.MaxValue;
            processed = false;
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
        
        static Quaternion
            northRotation = Quaternion.Euler(90f, 0f, 0f),
            eastRotation = Quaternion.Euler(90f, 90f, 0f),
            southRotation = Quaternion.Euler(90f, 180f, 0f),
            westRotation = Quaternion.Euler(90f, 270f, 0f);
        
        public void ShowPath() 
        {
            if (distance == 0 || !walkable || type is TileType.Spawn or TileType.Destination) 
            {
                arrow.gameObject.SetActive(false);
                return;
            }
            
            arrow.gameObject.SetActive(true);
            arrow.localRotation =
                next == north ? northRotation :
                next == east ? eastRotation :
                next == south ? southRotation :
                westRotation;
        }

        public void HidePath()
        {
            arrow.gameObject.SetActive(false);
        }

        public void InvertNeighbors()
        {
            if (north) north.alternative = !alternative;
            if (east) east.alternative = !alternative;
            if (south) south.alternative = !alternative;
            if (west) west.alternative = !alternative;
        }
    }
}
