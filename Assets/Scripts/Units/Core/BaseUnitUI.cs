using System;
using UnityEngine;
using UnityEngine.UI;

namespace Units
{
    public class BaseUnitUI : MonoBehaviour
    {
        [SerializeField] private Slider _healthSlider;
        [SerializeField] private BaseUnit _baseUnit;

        private void Awake()
        {
            _baseUnit.OnHealthChanged += SetHealthSliderValue;
            _healthSlider.minValue = 0;
            _healthSlider.maxValue = _baseUnit.StartHealth;

            SetHealthSliderValue(_baseUnit.StartHealth);
        }

        private void OnDestroy()
        {
            _baseUnit.OnHealthChanged -= SetHealthSliderValue;
        }

        private void SetHealthSliderValue(int health)
        {
            _healthSlider.value = health;
        }
    }
}