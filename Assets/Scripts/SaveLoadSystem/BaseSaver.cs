using System;
using UnityEngine;

namespace Startup
{
    [RequireComponent(typeof(IdentifierHolder))]
    public abstract class BaseSaver : MonoBehaviour
    {
        public event Action OnDeleted;

        [SerializeField] private IdentifierHolder _identifierHolder;

        public IdentifierHolder IdentifierHolder => _identifierHolder;

        public abstract string Save();

        public abstract void Load(string json);

        public void InvokeOnDeleted()
        {
            OnDeleted?.Invoke();
        }
    }
}