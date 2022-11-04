using System;
using UnityEngine;

namespace Scenarios
{
    [Serializable]
    public struct Wave
    {
        public Sequence[] spawnSequences;

        private int index;

        public void Init()
        {
            Debug.Assert(spawnSequences.Length > 0, "Empty wave!");
            index = 0;
            spawnSequences[index].Init();
        }

        public float Progress(float deltaTime)
        {
            deltaTime = spawnSequences[index].Progress(deltaTime);
            while(deltaTime >= 0f)
            {
                if (++index >= spawnSequences.Length)
				{
					return deltaTime;
				}
				spawnSequences[index].Init();
				deltaTime = spawnSequences[index].Progress(deltaTime);
            }

            return -1f;
        }
    }
}
