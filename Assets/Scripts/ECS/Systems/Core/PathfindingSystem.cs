using Events.Enemies;
using Leopotam.Ecs;
using Pathfinding;
using Services;

namespace Systems.Core
{
    public class PathfindingSystem : IEcsInitSystem, IEcsRunSystem
    {
        private BreadthFirstSearch _bfs;
        private readonly EcsWorld _world = null;

        private readonly GridData _gridData = null;

        private readonly EcsFilter<FindPathEvent> _findPathFilter = null;
        
        public void Init()
        {
            _bfs = new BreadthFirstSearch();
            _world.NewEntity().Get<FindPathEvent>();
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