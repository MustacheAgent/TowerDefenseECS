using Enums;
using Events.Enemies;
using Leopotam.Ecs;
using System;
using Voody.UniLeo;

namespace Scenarios
{
    [Serializable]
    public struct Sequence
    {
        public EnemyType type;
        public int amount;
        public float cooldown;

        private float _currentTime;
        private int _count;

        public void Init()
        {
            _currentTime = cooldown;
            _count = 0;
        }

        public float Progress(float deltaTime)
        {
            _currentTime += deltaTime;
            while(_currentTime >= cooldown)
            {
                _currentTime -= cooldown;
                if (_count >= amount) return _currentTime;
                _count += 1;
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
