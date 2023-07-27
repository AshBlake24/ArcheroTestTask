using System;
using UnityEngine;

namespace Source.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerStayed;
        
        private void OnTriggerStay(Collider other) => 
            TriggerStayed?.Invoke(other);
    }
}