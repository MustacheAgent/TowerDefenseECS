using Components.Core;
using Leopotam.Ecs;
using Tags;
using Unity.Collections;
using UnityEngine;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        readonly EcsFilter<PositionComponent, EmptyTileTag, SpawnTag, DestinationTag> filter = null;
        private SceneData _sceneData;

        public void Init()
        {
            SetGridSize();
        }

        public void Run()
        {

        }

        /// <summary>
        /// Вычисляет размер сетки игрового поля.
        /// </summary>
        private void SetGridSize()
        {
            int gridSizeZ = 1;
            int gridSizeX = 1;
            Vector3 raycastPos = _sceneData.worldBottomLeft.transform.position;

            // получение размера сетки по Z
            while (true)
            {
                // TODO: возможно надо увеличить расстояние луча (ведь клетки не всегда будут стоят вплотную)
                if (Physics.Raycast(raycastPos, Vector3.forward, out RaycastHit ray, 1))
                {
                    raycastPos = ray.collider.transform.position;
                    gridSizeZ++;
                }
                else
                {
                    break;
                }
            }

            // получение размера сетки по Х
            while (true)
            {
                if (Physics.Raycast(raycastPos, Vector3.right, out RaycastHit ray, 1))
                {
                    raycastPos = ray.collider.transform.position;
                    gridSizeX++;
                }
                else
                {
                    break;
                }
            }

            _sceneData.gridSizeX = gridSizeX;
            _sceneData.gridSizeZ = gridSizeZ;
        }
    }
}
