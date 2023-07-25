using System.Collections;
using Source.Utilities;
using UnityEngine;

namespace Source.Infrastructure
{
    public class LoadingScreen : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private float _fadeTime;
        [SerializeField] private float _updateTime;

        private void Awake() => 
            DontDestroyOnLoad(this);

        public void Hide() => 
            StartCoroutine(FadeOut());

        public void Show()
        {
            gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
        }

        private IEnumerator FadeOut()
        {
            while (_canvasGroup.alpha > _fadeTime)
            {
                _canvasGroup.alpha -= _fadeTime;
                yield return TimeUtility.GetTime(_updateTime);
            }

            gameObject.SetActive(false);
        }
    }
}