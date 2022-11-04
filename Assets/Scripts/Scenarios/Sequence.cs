using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using System;
using UnityEngine;
using Voody.UniLeo;

namespace Scenarios
{
    [Serializable]
    public struct Sequence
    {
        public EnemyType type;
        public int amount;
        public float cooldown;

        private float currentTime;
        private int count;

        public void Init()
        {
            currentTime = 0;
            count = 0;
        }

        public float Progress(float deltaTime)
        {
            currentTime += deltaTime;
            while(currentTime >= cooldown)
            {
                currentTime -= cooldown;
                if (count >= amount) return currentTime;
                count += 1;
                // создать противника
                WorldHandler.GetWorld().NewEntity().Get<SpawnEnemyEvent>() = new SpawnEnemyEvent
                {
                    type = this.type
                };
            }
            return -1f;
        }
    }
}
