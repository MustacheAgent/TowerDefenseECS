using Components.Core;
using Leopotam.Ecs;
using Tags;
using UnityEngine;

namespace Systems.Core
{
    public class CameraMoveSystem : IEcsRunSystem
    {
        readonly EcsFilter<CameraTag, PositionComponent, CameraSpeedComponent> cameraFilter = null;
        PlayerInputData _input = null;

        public void Run()
        {
            foreach(var i in cameraFilter)
            {
                ref var position = ref cameraFilter.Get2(i);
                ref var speed = ref cameraFilter.Get3(i).cameraSpeed;
                position.transform.Translate(speed * Time.deltaTime * new Vector3(_input.keyInput.x, 0.0f, _input.keyInput.z), Space.World);
            }
        }
    }
}
