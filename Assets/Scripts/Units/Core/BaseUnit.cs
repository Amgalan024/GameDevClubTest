using System;
using UnityEngine;

namespace Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;

        [SerializeField] private int _startHealth;

        private int _currentHealth;

        private void Awake()
        {
            _currentHealth = _startHealth;
        }

        public void ChangeHealth(int value)
        {
            _currentHealth += value;

            if (_currentHealth > _startHealth)
            {
                _currentHealth = _startHealth;
            }

            if (_currentHealth < 0)
            {
                _currentHealth = 0;
                OnDeath?.Invoke();
            }

            OnHealthChanged?.Invoke(_currentHealth);
        }
    }
}