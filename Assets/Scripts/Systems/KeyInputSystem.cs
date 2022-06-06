using Leopotam.Ecs;
using UnityEngine;

namespace Systems
{
    public class KeyInputSystem : IEcsRunSystem
    {
        public void Run()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Debug.Log("Pressed LMB!!!");
            }
        }
    }
}
