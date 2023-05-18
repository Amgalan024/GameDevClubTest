using UnityEngine;

namespace Projectile
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        public abstract void Shoot(Transform target);
    }
}