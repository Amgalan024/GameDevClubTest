using System;
using Inventory;
using Item;
using Projectile;
using TMPro;
using UnityEngine;

namespace Player
{
    public class PlayerAttackService : MonoBehaviour
    {
        [SerializeField] private Transform _weaponAnchor;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private TextMeshProUGUI _ammoText;

        private BaseWeapon _weapon;
        private Transform _target;
        private InventoryItem _requiredAmmo;

        private void Awake()
        {
            SetAmmoText(0);
        }

        public void Shoot()
        {
            if (_target == null)
            {
                return;
            }

            if (_inventoryService.TryRemoveItemAmount(_weapon.RequiredAmmo, 1))
            {
                _weapon.Shoot(_target);
            }
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }

        public void SetWeapon(BaseWeapon weapon)
        {
            if (_requiredAmmo != null)
            {
                _requiredAmmo.OnAmountChanged -= SetAmmoText;
            }

            _weapon = Instantiate(weapon, _weaponAnchor);
            _requiredAmmo = _inventoryService.GetItemByBehaviour(_weapon.RequiredAmmo);
            _requiredAmmo.OnAmountChanged += SetAmmoText;
            SetAmmoText(_requiredAmmo.Amount);
        }

        public bool IsWeaponEquipped()
        {
            return _weapon != null;
        }

        private void SetAmmoText(int value)
        {
            _ammoText.text = value.ToString();
        }
    }
}