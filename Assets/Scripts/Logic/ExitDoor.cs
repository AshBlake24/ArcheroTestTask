using Source.Gameplay;
using Source.Infrastructure.Events;
using UnityEngine;

namespace Source.Logic
{
    public class ExitDoor : MonoBehaviour, IStageClearHandler
    {
        private void OnEnable() => EventBus.Subscribe(this);

        private void OnDisable() => EventBus.Unsubscribe(this);
        
        public void OnStageCleared() => 
            gameObject.SetActive(false);
    }
}