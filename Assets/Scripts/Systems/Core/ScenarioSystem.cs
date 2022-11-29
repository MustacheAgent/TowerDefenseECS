using System.Text;
using Events.Scenario;
using Leopotam.Ecs;
using Scenarios;
using Services;
using UnityEngine;
using Voody.UniLeo;

namespace Systems.Core
{
    public class ScenarioSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly SceneData _sceneData = null;
        private readonly HudData _hudData = null;
        private Scenario _levelScenario;

        private readonly EcsFilter<WaveStartedEvent> _waveStartedFilter = null;
        private readonly EcsFilter<SequenceCompletedEvent> _seqCompletedFilter = null;
        private readonly EcsFilter<WaveCompletedEvent> _waveCompletedFilter = null;
        private readonly EcsFilter<BeginScenarioEvent> _beginScenarioFilter = null;

        public void Init()
        {
            _levelScenario = _sceneData.scenario;
            _levelScenario.Init();
            _hudData.waveNumber.text = string.Format("{0}/{1}", _levelScenario.WaveIndex, _levelScenario.waves.Length);
            _hudData.progressBar.value = 0;
            _hudData.progressBar.maxValue = _levelScenario.SeqLength;
            //WorldHandler.GetWorld().NewEntity().Get<BeginScenarioEvent>();
        }

        public void Run()
        {
            foreach (var eventIndex in _beginScenarioFilter)
            {
                _levelScenario.NextWave();
                _beginScenarioFilter.GetEntity(eventIndex).Del<BeginScenarioEvent>();
            }

            foreach (var eventIndex in _waveStartedFilter)
            {
                var waveNum = _waveStartedFilter.Get1(eventIndex).WaveNumber;
                _hudData.waveNumber.text = string.Format("{0}/{1}", waveNum, _levelScenario.waves.Length);
                _hudData.progressBar.value = 0;
                _hudData.progressBar.maxValue = _levelScenario.SeqLength;
                _waveStartedFilter.GetEntity(eventIndex).Del<WaveStartedEvent>();
            }
            
            foreach (var eventIndex in _waveCompletedFilter)
            {
                _levelScenario.PrepareNextWave();
                _waveCompletedFilter.GetEntity(eventIndex).Del<WaveCompletedEvent>();
            }
            
            foreach (var eventIndex in _seqCompletedFilter)
            {
                var seqNum = _seqCompletedFilter.Get1(eventIndex).SequenceNumber;
                _hudData.progressBar.value = seqNum;
                _seqCompletedFilter.GetEntity(eventIndex).Del<SequenceCompletedEvent>();
            }
        }
    }
}
