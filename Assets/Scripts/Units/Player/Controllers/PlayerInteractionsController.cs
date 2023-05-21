using System;
using Enemy;
using Inventory;
using Item;
using Units.Zones;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionsController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private TriggerZone _attackRangeZone;
        [SerializeField] private TriggerZone _itemPickUpZone;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private PlayerAttackService _playerAttackService;

        private void Awake()
        {
            _playerModel.OnDeath += OnPlayerDeath;
            _itemPickUpZone.OnZoneEnter += HandleItemInRange;

            _attackRangeZone.OnZoneEnter += HandleEnemyInRange;
            _attackRangeZone.OnZoneExit += HandleEnemyOutOfRange;
        }

        private void OnDestroy()
        {
            OnPlayerDeath();
        }

        private void OnPlayerDeath()
        {
            _playerModel.OnDeath -= OnPlayerDeath;
            _itemPickUpZone.OnZoneEnter -= HandleItemInRange;

            _attackRangeZone.OnZoneEnter -= HandleEnemyInRange;
            _attackRangeZone.OnZoneExit -= HandleEnemyOutOfRange;
        }

        private void HandleEnemyInRange(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out EnemyModel enemyModel))
            {
                _playerAttackService.AddTarget(enemyModel);
            }
        }

        private void HandleEnemyOutOfRange(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out EnemyModel enemyModel))
            {
                _playerAttackService.RemoveTarget(enemyModel);
            }
        }

        private void HandleItemInRange(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out DropItem item))
            {
                _inventoryService.AddItem(item.ItemBehaviourPrefab, item.Amount);

                item.PickUp();
            }
        }
    }
}