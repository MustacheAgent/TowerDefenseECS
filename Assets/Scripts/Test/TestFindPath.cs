using Pathfinding;
using UnityEngine;

namespace Test
{
    public class TestFindPath : MonoBehaviour
    {
        public Tile destination;
        
        private void Start()
        {
            var BFS = new BreadthFirstSearch();
            BFS.FindPath(destination);
        }
    }
}