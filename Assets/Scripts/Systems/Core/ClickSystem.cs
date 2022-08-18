using Leopotam.Ecs;
using Scripts;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class ClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private PlayerInputData _input = null;
        private SceneData _sceneData = null;

        public void Init()
        {
            
        }

        public void Run()
        {
            if (_input.leftMousePressed)
            {
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if(Physics.Raycast(ray, out RaycastHit hit))
                {
                    var gameObject = hit.transform.gameObject;
                    var entity = gameObject.GetEntity();
                    //Debug.Log("Clicked");
                }
            }
        }

        private int NodeFromPoint(Vector3 point)
        {
            int x = Mathf.RoundToInt(point.x);
            int y = Mathf.RoundToInt(point.z);
            Debug.Log("Point: " + point + "\n" + "Got indexes. X: " + x + " Z: " + y);
            if (x >= 0 && x < _sceneData.gridSizeX && y >= 0 && y < _sceneData.gridSizeZ)
            {
                return x + y * _sceneData.gridSizeX;
            }
            return -1;
        }
    }
}
