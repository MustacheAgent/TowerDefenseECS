using Leopotam.Ecs;
using Scenarios;

namespace Systems.Core
{
    public class ScenarioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private SceneData _sceneData = null;
        private Scenario levelScenario;

        public void Init()
        {
            levelScenario = _sceneData.scenario;
            levelScenario.Init();
        }

        public void Run()
        {
            levelScenario.Progress();
        }
    }
}
