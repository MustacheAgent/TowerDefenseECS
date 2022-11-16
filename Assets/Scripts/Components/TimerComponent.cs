using System;

namespace Components
{
    public struct TimerComponent
    {
        public bool IsPaused;
        public float Cooldown;
        public float Current;
        public Action Callback;
    }
}