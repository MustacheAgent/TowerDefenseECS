using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private BreadthFirstSearch _bfs;

        private readonly EcsFilter<FindPathEvent> _findPathFilter = null;
        private readonly EcsWorld _world = null;
        
        public void Init()
        {
            _bfs = new BreadthFirstSearch();
        }
        
        public void Run()
        {
            foreach (var i in _findPathFilter)
            {
                _findPathFilter.GetEntity(i).Del<FindPathEvent>();
            }
        }
    }
}