using System;
using Source.Camera;
using Source.Infrastructure;
using Source.Infrastructure.Services.Input;
using UnityEngine;

namespace Source.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private CharacterController _characterController;
        
        private IInputService _inputService;
        private Vector3 _direction;

        private void Awake()
        {
            _inputService = Game.s_InputService;
        }

        private void Start()
        {
            UnityEngine.Camera.main.GetComponent<CameraFollow>().SetTarget(transform);
        }

        private void Update()
        {
            if ((_inputService.Axis.sqrMagnitude > 0.01f) == false) 
                return;
            
            _direction = GetDirection();
            _direction.Normalize();
                
            RotateTowardsDirection();
            Move();
        }

        private Vector3 RotateTowardsDirection()
        {
            return transform.forward = _direction;
        }

        private void Move() => 
            _characterController.Move(_direction * _moveSpeed * Time.deltaTime);

        private Vector3 GetDirection()
        {
            Vector2 inputServiceAxis = _inputService.Axis;
            return new Vector3(inputServiceAxis.x, 0, inputServiceAxis.y);
        }
    }
}