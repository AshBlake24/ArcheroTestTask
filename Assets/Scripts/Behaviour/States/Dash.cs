using UnityEngine;

namespace Source.Behaviour.States
{
    public class Dash : IState
    {
        private readonly Transform _target;
        private readonly Transform _self;
        private readonly float _speed;

        private Vector3 _direction;

        public Dash(Transform target, Transform self, float speed)
        {
            _target = target;
            _self = self;
            _speed = speed;
        }

        public void Tick()
        {
            _self.position += _direction * _speed * Time.deltaTime;
        }

        public void OnEnter()
        {
            _direction = (_target.position - _self.position).normalized;
            _self.forward = _direction;
        }

        public void OnExit() { }
    }
}