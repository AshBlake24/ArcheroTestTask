using Source.Behaviour;
using UnityEngine;

namespace Source.Enemies
{
    public class Enemy : MonoBehaviour
    {
        private StateMachine _stateMachine;

        public void Construct()
        {
            _stateMachine = new StateMachine();
        }
        
        private void Update() => _stateMachine.Tick();
    }
}