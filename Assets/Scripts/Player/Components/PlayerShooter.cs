using Source.Combat;
using Source.Gameplay;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player.Components
{
    [RequireComponent(typeof(PlayerAim))]
    public class PlayerShooter : MonoBehaviour, IStartGameHandler
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectilePrefab;

        private IInputService _inputService;
        private float _elapsedTime;
        private float _attackRate;
        private float _attackForce;
        private int _damage;

        public void Construct(IInputService inputService, float attackRate, float attackForce, int damage)
        {
            enabled = false;
            _inputService = inputService;
            _attackRate = attackRate;
            _attackForce = attackForce;
            _damage = damage;
            
            EventBus.Subscribe(this);
        }

        private void OnDestroy() => EventBus.Unsubscribe(this);

        private void Update()
        {
            if (PlayerIsMoving() || _aim.HasTarget == false)
            {
                _elapsedTime = 0;
                return;
            }

            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _attackRate)
            {
                Shoot();
                _elapsedTime = 0;
            }
        }

        private void Shoot()
        {
            Projectile projectile = Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
            projectile.Init(_damage, _attackForce);
        }
        
        private bool PlayerIsMoving() => 
            _inputService.IsMoving;

        public void OnGameStarted() => 
            enabled = true;
    }
}