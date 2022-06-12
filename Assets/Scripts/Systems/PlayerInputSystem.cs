using Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        readonly EcsFilter<PlayerInputComponent> filter = null;

        public void Run()
        {
            foreach(var i in filter)
            {
                ref var input = ref filter.Get1(i);
                ref var keyInput = ref input.keyInput;
                ref var mousePos = ref input.mousePosition;

                keyInput.x = Input.GetAxis("Horizontal");
                keyInput.z = Input.GetAxis("Vertical");

                mousePos = Input.mousePosition;
            }
        }
    }
}
