using System;
using System.Collections.Generic;
using System.Linq;
using Inventory;
using Item;
using Projectile;
using TMPro;
using Units;
using UnityEngine;

namespace Player
{
    public class PlayerAttackService : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Transform _weaponAnchor;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private TextMeshProUGUI _ammoText;

        private BaseWeapon _weapon;
        private InventoryItem _requiredAmmo;

        private readonly List<BaseUnit> _targetsInRange = new List<BaseUnit>();

        private void Awake()
        {
            SetAmmoText(0);
        }

        public void Shoot()
        {
            if (_targetsInRange.Count <= 0 || _weapon == null)
            {
                return;
            }

            if (_inventoryService.TryRemoveItemAmount(_weapon.RequiredAmmo, 1))
            {
                var closestTarget =
                    _targetsInRange.OrderBy(t =>
                        t.transform.position.magnitude - _playerModel.transform.position.magnitude).First();

                _weapon.Shoot(closestTarget.transform);
            }
        }

        public void AddTarget(BaseUnit target)
        {
            _targetsInRange.Add(target);
        }

        public void RemoveTarget(BaseUnit target)
        {
            _targetsInRange.Remove(target);
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