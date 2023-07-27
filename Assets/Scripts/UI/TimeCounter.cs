using System.Collections;
using Source.Gameplay;
using Source.Infrastructure.Events;
using Source.Utilities;
using TMPro;
using UnityEngine;

namespace Source.UI
{
    public class TimeCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        
        public int _secondsToStart;

        public void Construct(int secondsToStart)
        {
            _secondsToStart = secondsToStart;
        }

        private void Start()
        {
            StartCoroutine(Countdown());
        }

        private IEnumerator Countdown()
        {
            while (_secondsToStart > 0)
            {
                _text.text = _secondsToStart.ToString();
                
                yield return TimeUtility.GetTime(1);
                
                _secondsToStart--;
            }
            
            EventBus.RaiseEvent<IStartGameHandler>(h => h.OnGameStarted());
        }
    }
}