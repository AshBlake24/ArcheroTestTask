using Source.Combat;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player
{
    [RequireComponent(typeof(PlayerAim))]
    public class PlayerShooter : MonoBehaviour
    {
        [SerializeField] private PlayerAim _aim;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private float _attackRate;

        private IInputService _inputService;
        private float _elapsedTime;

        private void Start()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
        }

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
            Instantiate(_projectilePrefab, _shootPoint.position, _shootPoint.rotation);
        }
        
        private bool PlayerIsMoving() => 
            _inputService.IsMoving;
    }
}