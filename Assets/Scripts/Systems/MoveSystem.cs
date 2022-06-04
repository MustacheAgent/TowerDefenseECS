using Leopotam.Ecs;

namespace TowerDefense 
{
    sealed class MoveSystem : IEcsRunSystem 
    {
        // auto-injected fields.
        readonly EcsWorld _world = null;
        
        void IEcsRunSystem.Run() 
        {
            // add your run code here.
        }
    }
}