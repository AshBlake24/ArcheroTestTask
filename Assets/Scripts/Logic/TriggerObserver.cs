using System;
using UnityEngine;

namespace Source.Logic
{
    [RequireComponent(typeof(Collider))]
    public class TriggerObserver : MonoBehaviour
    {
        public event Action<Collider> TriggerEnter;
    
        private void OnTriggerEnter(Collider other) => 
            TriggerEnter?.Invoke(other);
    }
}