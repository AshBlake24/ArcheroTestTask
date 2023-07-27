using Source.Data;
using TMPro;
using UnityEngine;

namespace Source.UI.Elements
{
    public class CoinsObserver : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coins;
        
        private Balance _balance;

        public void Construct(Balance balance)
        {
            _balance = balance;
            _balance.Changed += OnBalanceChanged;
        }

        private void OnDestroy() => 
            _balance.Changed -= OnBalanceChanged;

        private void Start() => OnBalanceChanged();

        private void OnBalanceChanged() => 
            _coins.text = _balance.Coins.ToString();
    }
}