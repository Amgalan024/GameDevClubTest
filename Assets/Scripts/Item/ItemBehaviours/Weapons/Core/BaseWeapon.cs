using Item;
using Item.Ammo;
using UnityEngine;

namespace Projectile
{
    public abstract class BaseWeapon : ItemBehaviour
    {
        [SerializeField] private Ammo _requiredAmmo;

        public Ammo RequiredAmmo => _requiredAmmo;

        public abstract void Shoot(Transform target);
    }
}