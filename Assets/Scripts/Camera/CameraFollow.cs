using System;
using UnityEngine;

namespace Source.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset;
        [SerializeField] private float _smoothTime;
        [SerializeField] private float _speed;

        private Transform _target;
        private Vector3 _velocity;

        public void SetTarget(Transform target)
        {
            if (target == null)
                throw new ArgumentNullException(nameof(target));
            
            _target = target;
        }

        private void LateUpdate()
        {
            if (_target == null)
                return;

            transform.position = Vector3.SmoothDamp(
                transform.position, 
                GetPosition(), 
                ref _velocity, 
                _smoothTime, 
                _speed);
        }

        private Vector3 GetPosition()
        {
            Vector3 position = _target.position;
            position.x = 0;
            position += _positionOffset;
            return position;
        }
    }
}