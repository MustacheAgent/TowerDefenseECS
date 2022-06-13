using Leopotam.Ecs;
using UnityEngine;

namespace Systems.Core
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        PlayerInputData _input = null;

        public void Run()
        {
            ref var keyInput = ref _input.keyInput;
            ref var mousePos = ref _input.mousePosition;

            keyInput.x = Input.GetAxis("Horizontal");
            keyInput.z = Input.GetAxis("Vertical");

            mousePos = Input.mousePosition;
        }
    }
}
