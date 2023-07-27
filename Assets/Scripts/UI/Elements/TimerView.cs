using Source.Logic;
using TMPro;
using UnityEngine;

namespace Source.UI.Elements
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        private Timer _timer;

        public void Construct(Timer timer)
        {
            _timer = timer;
            _timer.TimeChanged += OnTimeChanged;
        }

        private void OnTimeChanged(int seconds)
        {
            if (seconds <= 0)
                Destroy(gameObject);
            else
                _timerText.text = seconds.ToString();
        }
    }
}