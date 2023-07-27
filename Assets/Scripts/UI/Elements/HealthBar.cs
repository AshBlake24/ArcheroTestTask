using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Source.UI.Elements
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Image _currentHealthImage;

        public void SetValue(int current, int max) => 
            _currentHealthImage.fillAmount = (float) current / max;
    }
}