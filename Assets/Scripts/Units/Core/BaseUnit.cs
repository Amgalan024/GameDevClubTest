using System;
using UnityEngine;

namespace Units
{
    public abstract class BaseUnit : MonoBehaviour
    {
        public event Action<int> OnHealthChanged;
        public event Action OnDeath;

        [SerializeField] private int _startHealth;

        public int StartHealth => _startHealth;

        public int CurrentHealth { get; private set; }

        private void Awake()
        {
            CurrentHealth = _startHealth;
        }

        public void ChangeHealth(int value)
        {
            CurrentHealth += value;

            if (CurrentHealth > _startHealth)
            {
                CurrentHealth = _startHealth;
            }

            if (CurrentHealth < 0)
            {
                CurrentHealth = 0;
                OnDeath?.Invoke();
            }

            OnHealthChanged?.Invoke(CurrentHealth);
        }
    }
}