using UnityEngine;

namespace Source.Behaviour.States
{
    public class MoveTransformToTarget : IState
    {
        private const float SmoothTime = 0.05f;
        
        private readonly Transform _target;
        private readonly Transform _self;
        private readonly float _speed;

        private float _currentVelocity;
        private Vector3 _direction;

        public MoveTransformToTarget(Transform target, Transform self, float speed)
        {
            _target = target;
            _self = self;
            _speed = speed;
        }

        public void Tick()
        {
            GetDirection();
            Move();
            RotateTowardsTarget();
        }

        public void OnEnter() { }

        public void OnExit() { }

        private void GetDirection() => 
            _direction = (_target.position - _self.position).normalized;

        private void Move() => 
            _self.position += _direction * _speed * Time.deltaTime;

        private void RotateTowardsTarget()
        {
            float rotationAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(_self.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            _self.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }
    }
}