﻿using System.Timers;
using Source.Enemies;
using Source.Enemies.Data;
using UnityEngine;

namespace Source.Behaviour.States
{
    public class RangeAttack : IState
    {
        private const float SmoothTime = 0.05f;
        private const float FirePointHeight = 0.75f;
        private const string PlayerLayer = "Player";

        private readonly RangedEnemy _enemy;
        private readonly Transform _target;
        private readonly ArcherStaticData _enemyData;

        private float _elapsedTime;
        private float _currentVelocity;
        private LayerMask _playerLayerMask;
        
        public float IdleTime { get; private set; }

        public RangeAttack(RangedEnemy enemy, Transform target, ArcherStaticData enemyData)
        {
            _enemy = enemy;
            _target = target;
            _enemyData = enemyData;
            _playerLayerMask = LayerMask.NameToLayer(PlayerLayer);
        }
        
        public void Tick()
        {
            _elapsedTime += Time.deltaTime;
            
            if (LineOfFireIsFree(_target.position) == false)
            {
                IdleTime += Time.deltaTime;
                return;
            }
            
            RotateTowardsTarget();
            
            if (_elapsedTime >= _enemyData.AttackRate)
            {
                Shoot();
                _elapsedTime = 0;
                IdleTime = 0;
            }
        }

        public void OnEnter()
        {
            IdleTime = 0;
            _elapsedTime = 0;
        }

        public void OnExit() { }
        
        private void Shoot()
        {
            Object.Instantiate(_enemyData.ProjectilePrefab, _enemy.ShootPoint.position, _enemy.ShootPoint.rotation)
                .Init(_enemyData.Damage, _enemyData.AttackForce);
        }

        private void RotateTowardsTarget()
        {
            Vector3 direction = (_target.transform.position - _enemy.transform.position).normalized;
            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf
                .SmoothDampAngle( _enemy.transform.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            _enemy.transform.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }
        
        private bool LineOfFireIsFree(Vector3 targetPosition)
        {
            Vector3 raycastOrigin = _enemy.transform.position;
            raycastOrigin.y = FirePointHeight;
            targetPosition.y = FirePointHeight;
            Vector3 direction = targetPosition - raycastOrigin;
            
            if (Physics.Raycast(raycastOrigin, direction, out RaycastHit hit))
                return hit.transform.gameObject.layer == _playerLayerMask.value;

            return false;
        }
    }
}