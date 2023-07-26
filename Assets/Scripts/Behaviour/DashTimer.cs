using System;
using System.Collections;
using Source.Utilities;
using UnityEngine;

namespace Source.Behaviour
{
    public class DashTimer : MonoBehaviour
    {
        private float _dashSpeed;
        private float _dashDuration;
        private float _dashRate;
        private float _elapsedTime;
        private bool _isDashing;

        public event Action<float> DashStarted;
        public event Action DashEnded;

        public void Construct(float dashDuration, float dashSpeed, float dashRate)
        {
            _dashDuration = dashDuration;
            _dashSpeed = dashSpeed;
            _dashRate = dashRate;
            _elapsedTime = 0f;
            _isDashing = false;
        }

        private void Update()
        {
            if (_isDashing)
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

        private void StartDash()
        {
            _isDashing = true;
            DashStarted?.Invoke(_dashSpeed);
        }

        private void EndDash()
        {
            _isDashing = false;
            DashEnded?.Invoke();
        }
    }
}