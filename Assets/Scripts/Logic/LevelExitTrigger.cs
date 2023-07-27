using Source.Infrastructure.Services;
using Source.Infrastructure.States;
using Source.Player.Components;
using UnityEngine;

namespace Source.Logic
{
    public class LevelExitTrigger : MonoBehaviour
    {
        private const string PlayerTag = "Player";
        
        [SerializeField] private string _levelName;
        
        private IGameStateMachine _stateMachine;
        private bool _isTriggered;

        private void Awake() => 
            _stateMachine = ServiceLocator.Container.Single<IGameStateMachine>();

        private void OnTriggerEnter(Collider other)
        {
            if (_isTriggered)
                return;

            PlayerHealth player = other.GetComponentInChildren<PlayerHealth>();

            if (player != null)
            {
                _stateMachine.Enter<LoadLevelState, string>(_levelName);
                _isTriggered = true;
            }
        }
    }
}