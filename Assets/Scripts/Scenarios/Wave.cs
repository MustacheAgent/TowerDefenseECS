using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    public struct Wave
    {
        public Sequence[] spawnSequences;

        private int _index;

        public void Init()
        {
            Debug.Assert(spawnSequences.Length > 0, "Empty wave!");
            _index = 0;
            spawnSequences[_index].Init();
        }

        public float Progress(float deltaTime)
        {
            deltaTime = spawnSequences[_index].Progress(deltaTime);
            while(deltaTime >= 0f)
            {
                if (++_index >= spawnSequences.Length)
				{
					return deltaTime;
				}
				spawnSequences[_index].Init();
				deltaTime = spawnSequences[_index].Progress(deltaTime);
            }

            return -1f;
        }
    }
}
