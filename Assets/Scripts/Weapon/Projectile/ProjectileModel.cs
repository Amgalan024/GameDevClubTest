using UnityEditor.UIElements;
using UnityEngine;

namespace Projectile
{
    public class ProjectileModel : MonoBehaviour
    {
        [SerializeField] private int _damage;
        [SerializeField] private float _speed;
        
        public int Damage => _damage;
        public float Speed => _speed;
    }
}