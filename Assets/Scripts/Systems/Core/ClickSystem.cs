using Components;
using Leopotam.Ecs;
using Tags;
using UnityEngine;
using Voody.UniLeo;

namespace Systems.Core
{
    public class ClickSystem : IEcsInitSystem, IEcsRunSystem
    {
        private PlayerInputData _input = null;
        private EcsFilter<CameraTag> filter = null;
        private EcsFilter<EmptyTileTag> tiles = null;
        private EcsEntity entity;

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
                    var data = gameObject.GetComponent<ConvertToEntity>();
                    if (data.TryGetEntity().HasValue)
                    {
                        entity = data.TryGetEntity().Value;
                    }

                    ref var path = ref entity.Get<PathfindingComponent>();

                    Debug.Log("Got entity. X: " + path.x + " Z : " + path.z);
                }
            }
        }
    }
}
