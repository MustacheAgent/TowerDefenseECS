using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
using Services;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private BreadthFirstSearch _bfs;

        private readonly GridData _gridData = null;

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
                if (!_bfs.FindPath(_gridData.destinationTiles))
                {
                    _bfs.FindPath(_gridData.destinationTiles, true);
                }
            }
        }
    }
}