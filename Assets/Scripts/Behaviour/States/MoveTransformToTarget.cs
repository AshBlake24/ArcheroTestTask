using UnityEngine;

namespace Source.Behaviour.States
{
    public class MoveTransformToTarget : MoveToTarget
    {
        private const float SmoothTime = 0.05f;
        
        private float _currentVelocity;
        private Vector3 _direction;

        public MoveTransformToTarget(Transform target, Transform self, float speed) : base(target, self, speed) { }

        public override void Tick()
        {
            GetDirection();
            Move();
            RotateTowardsTarget();
        }

        private void GetDirection() => 
            _direction = (Target.position - Self.position).normalized;

        private void Move() => 
            Self.position += _direction * CurrentSpeed * Time.deltaTime;

        private void RotateTowardsTarget()
        {
            float rotationAngle = Mathf.Atan2(_direction.x, _direction.z) * Mathf.Rad2Deg;
            float rotationAngleSmooth = Mathf.SmoothDampAngle(Self.eulerAngles.y, rotationAngle, ref _currentVelocity, SmoothTime);

            Self.rotation = Quaternion.Euler(0, rotationAngleSmooth, 0);
        }
    }
}