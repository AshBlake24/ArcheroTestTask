using System;
using UnityEngine;

namespace Source.Camera
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Vector3 _positionOffset;
        
        private Transform _target;

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

            transform.position = GetPosition();
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