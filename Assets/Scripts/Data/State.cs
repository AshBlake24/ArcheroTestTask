using System;

namespace Source.Data
{
    [Serializable]
    public class State
    {
        public int CurrentHealth;
        public int MaxHealth;

        public void Reset() => CurrentHealth = MaxHealth;
    }
}