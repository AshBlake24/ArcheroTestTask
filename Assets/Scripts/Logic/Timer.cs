using System;
using System.Collections;
using Source.Gameplay;
using Source.Infrastructure;
using Source.Infrastructure.Events;
using Source.Utilities;

namespace Source.Logic
{
    public class Timer
    {
        private readonly ICoroutineRunner _coroutineRunner;
        
        private int _secondsLeft;

        public event Action<int> TimeChanged; 

        public Timer(ICoroutineRunner coroutineRunner, int secondsLeft)
        {
            _coroutineRunner = coroutineRunner;
            _secondsLeft = secondsLeft; 
        }

        public void Start() => _coroutineRunner.StartCoroutine(Countdown());

        private IEnumerator Countdown()
        {
            while (_secondsLeft > 0)
            {
                TimeChanged?.Invoke(_secondsLeft);
                
                yield return TimeUtility.GetTime(1);
                
                _secondsLeft--;
            }

            TimeChanged?.Invoke(_secondsLeft);
            EventBus.RaiseEvent<IStartGameHandler>(h => h.OnGameStarted());
        }
    }
}