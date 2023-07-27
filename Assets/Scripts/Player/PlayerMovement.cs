using Source.Gameplay;
using Source.Infrastructure.Events;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player
{
    public class PlayerMovement : MonoBehaviour, IStartGameHandler
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _inputService;
        private Vector3 _direction;

        public void Construct(IInputService inputService, float playerDataMovementSpeed)
        {
            enabled = false;
            _inputService = inputService;
            EventBus.Subscribe(this);
        }

        private void OnDestroy() => EventBus.Unsubscribe(this);

        private void Update()
        {
            if (_inputService.IsMoving == false) 
                return;
            
            _direction = GetDirection();
            _direction.Normalize();
                
            RotateTowardsDirection();
            Move();
        }

        private Vector3 RotateTowardsDirection() => 
            transform.forward = _direction;

        private void Move() => 
            _characterController.Move(_direction * _moveSpeed * Time.deltaTime);

        private Vector3 GetDirection()
        {
            Vector2 inputServiceAxis = _inputService.Axis;
            return new Vector3(inputServiceAxis.x, 0, inputServiceAxis.y);
        }

        public void OnGameStarted() => 
            enabled = true;
    }
}