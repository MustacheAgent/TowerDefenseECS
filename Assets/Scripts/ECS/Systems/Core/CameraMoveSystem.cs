using Components.Core;
using ECS.Components.Core;
using ECS.Tags;
using Leopotam.Ecs;
using Services;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class CameraMoveSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraTag, PositionComponent, CameraSpeedComponent> _cameraFilter = null;
        private readonly PlayerInputData _input = null;

        public void Run()
        {
            foreach(var i in _cameraFilter)
            {
                ref var position = ref _cameraFilter.Get2(i).transform;
                ref var speed = ref _cameraFilter.Get3(i).cameraSpeed;
                ref var bounds = ref _cameraFilter.Get3(i).bounds;

                Vector3 newPos = position.position;
                newPos += new Vector3(_input.keyInput.x, 0.0f, _input.keyInput.z) * speed * Time.deltaTime;
                newPos.x = Mathf.Clamp(newPos.x, bounds.xMin, bounds.xMax);
                newPos.z = Mathf.Clamp(newPos.z, bounds.yMin, bounds.yMax);

                position.position = newPos;
                //position.Translate(speed * Time.deltaTime * newPos, Space.World);
            }
        }
    }
}
