using Components;
using Leopotam.Ecs;
using System;
using Tags;
using Unity.Mathematics;
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
        private SceneData _sceneData;

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

                    Debug.Log("Got entity from world. X: " + path.x);
                    path.x = 1350;

                    ref var pathf = ref _sceneData.tiles[0,0].Get<PathfindingComponent>();
                    Debug.Log("Got entity from cache. X: " + pathf.x);
                    //int2 node = NodeFromPoint(gameObject.transform.position);
                    //Debug.Log("Got entity. X: " + node.x + " Z : " + node.y);
                }
            }
        }

        private int2 NodeFromPoint(Vector3 point)
        {
            float percentX = (point.x + _sceneData.gridSizeX / 4f) / _sceneData.gridSizeX;
            float percentZ = (point.z + _sceneData.gridSizeZ / 4f) / _sceneData.gridSizeZ;

            percentX = Mathf.Clamp01(percentX);
            percentZ = Mathf.Clamp01(percentZ);

            int x = Mathf.RoundToInt((_sceneData.gridSizeX - 1) * percentX);
            int z = Mathf.RoundToInt((_sceneData.gridSizeZ - 1) * percentZ);
            return new int2(x, z);
        }
    }
}
