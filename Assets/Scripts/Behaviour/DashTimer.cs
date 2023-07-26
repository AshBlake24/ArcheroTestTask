using System.Collections;
using Source.Utilities;
using UnityEngine;

namespace Source.Behaviour
{
    public class DashTimer : MonoBehaviour
    {
        private float _dashDuration;
        private float _dashRate;
        private float _elapsedTime;
        
        public bool IsDashing { get; private set; }

        public void Construct(float dashDuration, float dashRate)
        {
            _dashDuration = dashDuration;
            _dashRate = dashRate;
            _elapsedTime = 0f;
            IsDashing = false;
        }

        private void Update()
        {
            if (IsDashing)
                return;
            
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _dashRate)
            {
                _elapsedTime = 0f;
                StartCoroutine(Dash());
            }
        }

        private IEnumerator Dash()
        {
            StartDash();

            yield return TimeUtility.GetTime(_dashDuration);

            EndDash();
        }

        private void StartDash() => IsDashing = true;

        private void EndDash() => IsDashing = false;
    }
}