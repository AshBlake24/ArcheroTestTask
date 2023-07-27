using Source.Logic;
using Source.UI.Elements;
using UnityEngine;

namespace Source.UI
{
    public class ActorUI : MonoBehaviour
    {
        [SerializeField] private HealthBar _healthBar;

        private IHealth _health;

        public void Construct(IHealth health)
        {
            _health = health;
            _health.HealthChanged += OnHealthChanged;
        }

        private void OnDestroy() =>
            _health.HealthChanged -= OnHealthChanged;

        private void OnHealthChanged() => 
            _healthBar.SetValue(_health.CurrentValue, _health.MaxValue);
    }
}