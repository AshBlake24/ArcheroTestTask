using Source.Enemies;
using Source.Infrastructure.Services;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player
{
    public class PlayerAim : MonoBehaviour
    {
        private const float MinimalVelocity = 0.1f;
        
        [SerializeField] private LayerMask _damageableLayerMask;
        [SerializeField] private float _updatesPerSecond;
        [SerializeField] private float _firePointHeight;
        [SerializeField, Min(0)] private float _radius;

        private readonly Collider[] _colliders = new Collider[6];
        private IInputService _inputService;
        public EnemyHealth _closetEnemy;

        public bool HasTarget => _closetEnemy != null;

        private void Start()
        {
            _inputService = ServiceLocator.Container.Single<IInputService>();
            
            InvokeRepeating(
                nameof(CheckTargets),
                time: 2,
                repeatRate: (1 / _updatesPerSecond));
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
            _inputService.Axis.sqrMagnitude > MinimalVelocity;
    }
}