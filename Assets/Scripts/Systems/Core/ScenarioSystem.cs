using System.Text;
using Events.Scenario;
using Leopotam.Ecs;
using Scenarios;
using Services;

namespace Systems.Core
{
    public class ScenarioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly HudData _hudData = null;
        private Scenario _levelScenario;

        private readonly EcsFilter<WaveCompletedEvent> _waveCompletedFilter = null;
        private readonly EcsFilter<SequenceCompletedEvent> _seqCompletedFilter = null;

        public void Init()
        {
            _levelScenario = _sceneData.scenario;
            _levelScenario.Init();
            _hudData.waveNumber.text = string.Format("{0}/{1}", _levelScenario.WaveIndex ,_levelScenario.waves.Length);
            _hudData.progressBar.value = 0;
            _hudData.progressBar.maxValue = _levelScenario.SeqLength;
        }

        public void Run()
        {
            _levelScenario.Progress();

            foreach (var eventIndex in _seqCompletedFilter)
            {
                var seqNum = _seqCompletedFilter.Get1(eventIndex).SequenceNumber;
                _hudData.progressBar.value = seqNum;
                _seqCompletedFilter.GetEntity(eventIndex).Del<SequenceCompletedEvent>();
            }
            
            foreach (var eventIndex in _waveCompletedFilter)
            {
                var waveNum = _waveCompletedFilter.Get1(eventIndex).WaveNumber;
                var wave = new StringBuilder();
                wave.Append(waveNum).Append("/").Append(_levelScenario.waves.Length);
                _hudData.waveNumber.text = wave.ToString();
                _hudData.progressBar.value = 0;
                _hudData.progressBar.maxValue = _levelScenario.SeqLength;
                _waveCompletedFilter.GetEntity(eventIndex).Del<WaveCompletedEvent>();
            }
        }
    }
}
