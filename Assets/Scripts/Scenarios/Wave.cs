using System;
using Components;
using Events.Enemies;
using Events.Scenario;
using Leopotam.Ecs;
using UnityEngine;
using Voody.UniLeo;

namespace Scenarios
{
    [Serializable]
    public struct Wave
    {
        public Sequence[] spawnSequences;
        public int delayBeforeNextWave;

        private int _seqIndex;
        
        public int SeqLength => spawnSequences.Length;

        public void Start()
        {
            Debug.Assert(spawnSequences.Length > 0, "Empty wave!");
            _seqIndex = 0;
            spawnSequences[_seqIndex].Init();
            Spawn();
        }

        public void Spawn()
        {
            spawnSequences[_seqIndex].Spawn();
            WorldHandler.GetWorld().NewEntity().Get<SequenceCompletedEvent>() = new SequenceCompletedEvent
            {
                SequenceNumber = _seqIndex + 1
            };
            
            if (++_seqIndex >= spawnSequences.Length)
            {
                WorldHandler.GetWorld().NewEntity().Get<WaveCompletedEvent>();
                return;
            }

            var timer = new TimerComponent
            {
                Cooldown = spawnSequences[_seqIndex].delayBeforeSpawn
            };
            timer.Callback += Spawn;
            WorldHandler.GetWorld().NewEntity().Get<TimerComponent>() = timer;
        }

        public float Progress(float deltaTime)
        {
            deltaTime = spawnSequences[_seqIndex].Progress(deltaTime);
            while(deltaTime >= 0f)
            {
                WorldHandler.GetWorld().NewEntity().Get<SequenceCompletedEvent>() = new SequenceCompletedEvent
                {
                    SequenceNumber = _seqIndex + 1
                };
                
                if (++_seqIndex >= spawnSequences.Length)
                {
                    return deltaTime;
                }
				spawnSequences[_seqIndex].Init();
				deltaTime = spawnSequences[_seqIndex].Progress(deltaTime);
            }

            return -1f;
        }
    }
}
