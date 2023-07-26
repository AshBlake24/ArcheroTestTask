using System;
using UnityEngine;

namespace Source.Behaviour.States
{
    public abstract class MoveToTarget : IState
    {
        protected readonly Transform Target;
        protected readonly Transform Self;
        private readonly float _initialSpeed;
        
        protected float CurrentSpeed;
        
        public float RemainingDistance => Vector3.Distance(Self.transform.position, Target.position);

        protected MoveToTarget(Transform target, Transform self, float speed)
        {
            Target = target;
            Self = self;
            _initialSpeed = speed;
        }
        
        public virtual void Tick() { }

        public virtual void OnEnter() => 
            SetSpeed(_initialSpeed);

        public virtual void OnExit() =>
            ResetSpeed();

        public void SetSpeed(float speed)
        {
            if (speed <= 0)
                throw new ArgumentOutOfRangeException(nameof(speed), "Speed can't be less or equal 0");

            CurrentSpeed = speed;
        }

        public void ResetSpeed() => 
            CurrentSpeed = _initialSpeed;
    }
}