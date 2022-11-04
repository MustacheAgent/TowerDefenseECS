using Enums;
using System;

namespace Scenarios
{
    [Serializable]
    public struct Sequence
    {
        public EnemyType type;
        public int amount;
        public float cooldown;

        public void Progress()
        {

        }
    }
}
