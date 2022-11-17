using Leopotam.Ecs;
using Services;
using UnityEngine;

namespace Systems.Core
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly PlayerInputData _input = null;

        public void Run()
        {
            ref var keyInput = ref _input.keyInput;
            ref var mousePos = ref _input.mousePosition;
            ref var isLmb = ref _input.leftMousePressed;
            ref var isRmb = ref _input.rightMousePressed;

            keyInput.x = Input.GetAxisRaw("Horizontal");
            keyInput.z = Input.GetAxisRaw("Vertical");

            mousePos = Input.mousePosition;
            isLmb = Input.GetKeyDown(KeyCode.Mouse0);
            isRmb = Input.GetKeyDown(KeyCode.Mouse1);
        }
    }
}
