using Source.Enemies;
using Source.Gameplay;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player.Components
{
    public class PlayerAim : MonoBehaviour, IStartGameHandler
    {
        private const float SmoothTime = 0.05f;
        
        [SerializeField] private LayerMask _damageableLayerMask;
        [SerializeField] private float _updatesPerSecond;
        [SerializeField] private float _firePointHeight;
        [SerializeField, Min(0)] private float _radius;

        private readonly Collider[] _colliders = new Collider[6];
        private IInputService _inputService;
        private EnemyHealth _closetEnemy;
        private float _currentVelocity;

        public bool HasTarget => _closetEnemy != null;

        public void Construct(IInputService inputService)
        {
            enabled = false;
            _inputService = inputService;
            EventBus.Subscribe(this);
        }

        private void OnDestroy() => EventBus.Unsubscribe(this);

        private void Update()
        {
            if (HasTarget == false)
                return;

            RotateTowardsTarget();
        }

        private void RotateTowardsTarget()
        {
            Vector3 direction = (_closetEnemy.transform.position - transform.position).normalized;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            transform.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }

        private void CheckTargets()
        {
            _closetEnemy = null;
            
            if (PlayerIsMoving())
                return;

            int collidersInArea = Physics.OverlapSphereNonAlloc(
                transform.position,
                _radius,
                _colliders,
                _damageableLayerMask);

            if (collidersInArea > 0)
                FindClosestTarget();
        }

        private void FindClosestTarget()
        {
            float closestEnemyDistance = float.MaxValue;

            foreach (Collider collider in _colliders)
            {
                if (collider == null)
                    continue;

                if (collider.gameObject.TryGetComponent(out EnemyHealth enemyHealth)
                    && LineOfFireIsFree(enemyHealth.transform.position))
                {
                    if (enemyHealth.CurrentValue <= 0)
                        continue;
                    
                    float distanceToEnemy = Vector3.Distance(transform.position, enemyHealth.transform.position);

                    if (distanceToEnemy < closestEnemyDistance)
                    {
                        closestEnemyDistance = distanceToEnemy;
                        _closetEnemy = enemyHealth;
                    }
                }
            }
        }

        private bool LineOfFireIsFree(Vector3 enemyPosition)
        {
            Vector3 raycastOrigin = transform.position;
            raycastOrigin.y = _firePointHeight;
            enemyPosition.y = _firePointHeight;
            Vector3 direction = enemyPosition - raycastOrigin;
            
            if (Physics.Raycast(raycastOrigin, direction, out RaycastHit hit, _radius))
                return (1 << hit.transform.gameObject.layer) == _damageableLayerMask.value;

            return false;
        }

        private bool PlayerIsMoving() => 
            _inputService.IsMoving;

        public void OnGameStarted()
        {
            enabled = true;
            
            InvokeRepeating(
                nameof(CheckTargets),
                time: 0,
                repeatRate: (1 / _updatesPerSecond));
        }
    }
}