using UnityEngine;

namespace Pathfinding
{
    public class Tile : MonoBehaviour
    {
        public Transform arrow;
        
        [Header("Breadth-First Search")]
        [HideInInspector] public bool processed;
        public bool walkable = true;
        //public List<Tile> neighbourTiles;
        //[HideInInspector] 
        public Tile north, west, east, south, next;
        public int distance;

        private void Awake()
        {
            GetNeighbors();
        }

        public void Reset()
        {
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
        
        static Quaternion
            northRotation = Quaternion.Euler(90f, 0f, 0f),
            eastRotation = Quaternion.Euler(90f, 90f, 0f),
            southRotation = Quaternion.Euler(90f, 180f, 0f),
            westRotation = Quaternion.Euler(90f, 270f, 0f);
        
        public void ShowPath() 
        {
            if (distance == 0 || !walkable) 
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
    }
}
