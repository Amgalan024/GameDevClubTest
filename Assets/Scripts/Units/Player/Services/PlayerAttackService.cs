using Inventory;
using Projectile;
using UnityEditor.UIElements;
using UnityEngine;

namespace Player
{
    public class PlayerAttackService : MonoBehaviour
    {
        [SerializeField] private Transform _weaponAnchor;
        [SerializeField] private InventoryService _inventoryService;

        private BaseWeapon _weapon;
        private Transform _target;

        public void Shoot()
        {
            if (_target == null)
            {
                return;
            }

            _weapon.Shoot(_target);
            _inventoryService.RemoveItemAmount(_weapon.RequiredAmmo, 1);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetWeapon(BaseWeapon weapon)
        {
            _weapon = Instantiate(weapon, _weaponAnchor);
        }

        public bool IsWeaponEquipped()
        {
            return _weapon != null;
        }
    }
}