using System;

namespace Source.Logic
{
    public interface IHealth
    {
        event Action HealthChanged;
        
        int CurrentValue { get; }
        int MaxValue { get; }

        void Construct(int health);
        void TakeDamage(int damage);
    }
}