using System.Linq;
using UnityEngine;

namespace Units
{
    public class UnitServiceProvider : MonoBehaviour
    {
        [SerializeField] private MonoBehaviour[] _services;

        public T GetService<T>() where T : MonoBehaviour
        {
            return (T) _services.First(s => s.GetType() == typeof(T));
        }
    }
}