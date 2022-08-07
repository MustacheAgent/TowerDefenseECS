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
            ref var isLMB = ref _input.leftMousePressed;

            keyInput.x = Input.GetAxis("Horizontal");
            keyInput.z = Input.GetAxis("Vertical");

            mousePos = Input.mousePosition;
            isLMB = Input.GetKeyDown(KeyCode.Mouse0);
        }
    }
}
