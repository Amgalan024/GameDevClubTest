using Projectile;
using UnityEngine;

namespace Player
{
    public class PlayerAttackService : MonoBehaviour
    {
        [SerializeField] private BaseWeapon _weapon;

        private Transform _target;

        public void Shoot()
        {
            if (_target == null)
            {
                return;
            }

            _weapon.Shoot(_target);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}