using UnityEngine;

namespace Startup
{
    [RequireComponent(typeof(IdentifierHolder))]
    public abstract class BaseSaver : MonoBehaviour
    {
        [SerializeField] private IdentifierHolder _identifierHolder;

        public IdentifierHolder IdentifierHolder => _identifierHolder;

        public abstract string Save();

        public abstract void Load(string saveData);
    }
}