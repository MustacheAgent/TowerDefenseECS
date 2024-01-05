using Components;
using ECS.Components;
using ECS.Tags;
using Events;
using Events.Enemies;
using Events.Scenario;
using Leopotam.Ecs;
using Services;
using Tags;

namespace Systems.Core
{
    public class GameProgressionSystem : IEcsRunSystem
    {
        private EcsFilter<ScenarioCompletedEvent> _victoryFilter;
        private EcsFilter<ReachedBaseEvent> _reachedFilter;
        private EcsFilter<EnemyTag> _enemyFilter;

        private readonly EcsWorld _world = null;

        private readonly HudData _hudData = null;
        private readonly SceneData _sceneData = null;
        
        public void Run()
        {
            if (_sceneData.baseHealth <= 0)
            {
                // поражение
                _world.NewEntity().Get<TimerComponent>() = new TimerComponent
                {
                    Cooldown = 3,
                    Callback = Defeat
                };
            }
            
            foreach (var eventIndex in _reachedFilter)
            {
                _world.NewEntity().Get<BaseHealthChangedEvent>() = new BaseHealthChangedEvent
                {
                    HealthChanged = -1
                };
                _reachedFilter.GetEntity(eventIndex).Del<ReachedBaseEvent>();
            }
            
            foreach (var eventIndex in _victoryFilter)
            {
                // победа
                if (!_enemyFilter.IsEmpty()) continue;
                _victoryFilter.GetEntity(eventIndex).Del<ScenarioCompletedEvent>();
                _hudData.gameOverMenu.ShowMenu(true);
            }
        }

        private void Defeat()
        {
            _hudData.gameOverMenu.ShowMenu(false);
        }
    }
}