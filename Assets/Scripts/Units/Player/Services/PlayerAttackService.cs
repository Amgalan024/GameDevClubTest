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

        private BaseWeapon _weaponBehaviour;
        private InventoryItem _inventoryAmmoItem;
        private InventoryItem _inventoryWeaponItem;

        private readonly List<BaseUnit> _targetsInRange = new List<BaseUnit>();

        private void Awake()
        {
            SetInventoryAmmoItemText(0);
            _inventoryService.OnItemAdded += OnItemAdded;
            _inventoryService.OnItemRemoved += OnItemRemoved;
        }

        private void OnDestroy()
        {
            _inventoryService.OnItemAdded -= OnItemAdded;
            _inventoryService.OnItemRemoved -= OnItemRemoved;
        }

        public void Shoot()
        {
            if (_targetsInRange.Count <= 0 || IsWeaponEquipped() == false)
            {
                return;
            }

            if (_inventoryService.TryRemoveItemAmount(_weaponBehaviour.RequiredAmmo, 1))
            {
                var closestTarget =
                    _targetsInRange.OrderBy(t =>
                        t.transform.position.magnitude - _playerModel.transform.position.magnitude).First();

                _weaponBehaviour.Shoot(closestTarget.transform);
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

        public void SetWeapon(BaseWeapon weapon, InventoryItem inventoryWeapon)
        {
            if (_inventoryAmmoItem != null)
            {
                _inventoryAmmoItem.OnAmountChanged -= SetInventoryAmmoItemText;
            }

            _inventoryWeaponItem = inventoryWeapon;

            _weaponBehaviour = Instantiate(weapon, _weaponAnchor);

            _inventoryAmmoItem = _inventoryService.GetItemByBehaviour(_weaponBehaviour.RequiredAmmo);

            if (_inventoryAmmoItem != null)
            {
                _inventoryAmmoItem.OnAmountChanged += SetInventoryAmmoItemText;
                SetInventoryAmmoItemText(_inventoryAmmoItem.Amount);
            }
            else
            {
                SetInventoryAmmoItemText(0);
            }
        }

        public bool IsWeaponEquipped()
        {
            return _weaponBehaviour != null;
        }

        private void OnItemAdded(InventoryItem inventoryItem)
        {
            if (_inventoryAmmoItem == null && IsWeaponEquipped() &&
                inventoryItem.ItemBehaviour == _weaponBehaviour.RequiredAmmo)
            {
                _inventoryAmmoItem = inventoryItem;
                _inventoryAmmoItem.OnAmountChanged += SetInventoryAmmoItemText;
                SetInventoryAmmoItemText(_inventoryAmmoItem.Amount);
            }
        }

        private void OnItemRemoved(InventoryItem inventoryItem)
        {
            if (_inventoryAmmoItem == inventoryItem)
            {
                _inventoryAmmoItem = null;
            }

            if (_inventoryWeaponItem == inventoryItem)
            {
                _inventoryWeaponItem = null;

                Destroy(_weaponBehaviour.gameObject);
            }
        }

        private void SetInventoryAmmoItemText(int value)
        {
            _ammoText.text = value.ToString();
        }
    }
}