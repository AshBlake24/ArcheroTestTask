using Source.Combat;
using Source.Gameplay;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services.Input;
using Source.Utilities;
using UnityEngine;

namespace Source.Player.Components
{
    [RequireComponent(typeof(PlayerAim))]
    public class PlayerShooter : MonoBehaviour, IStartGameHandler
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private Transform _shootPoint;

        private ObjectPool<Projectile> _projectilesPool;
        private IInputService _inputService;
        private float _elapsedTime;
        private float _attackRate;
        private float _attackForce;
        private int _damage;

        public void Construct(IInputService inputService, float attackRate, float attackForce, int damage, 
            Projectile projectilePrefab)
        {
            enabled = false;
            _projectilesPool = new ObjectPool<Projectile>(projectilePrefab.gameObject);
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
            Projectile projectile = _projectilesPool.GetInstance();
            projectile.transform.SetPositionAndRotation(_shootPoint.position, _shootPoint.rotation);
            projectile.gameObject.SetActive(true);
            projectile.Init(_damage, _attackForce);
        }
        
        private bool PlayerIsMoving() => 
            _inputService.IsMoving;

        public void OnGameStarted() => 
            enabled = true;
    }
}