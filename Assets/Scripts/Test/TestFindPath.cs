using Pathfinding;
using UnityEngine;

namespace Test
{
    public class TestFindPath : MonoBehaviour
    {
        public Tile[] destinations;
        
        private void Start()
        {
            new BreadthFirstSearch().FindPath(destinations);
        }
    }
}