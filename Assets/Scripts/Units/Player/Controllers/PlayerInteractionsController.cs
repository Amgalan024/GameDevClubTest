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
        [SerializeField] private TriggerZone _interactionZone;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private PlayerAttackService _playerAttackService;

        private void Awake()
        {
            _playerModel.OnDeath += OnPlayerDeath;
            _interactionZone.OnZoneEnter += HandleItemInteraction;
            _interactionZone.OnZoneEnter += HandleEnemyInteraction;
        }

        private void OnDestroy()
        {
            OnPlayerDeath();
        }

        private void OnPlayerDeath()
        {
            _playerModel.OnDeath -= OnPlayerDeath;
            _interactionZone.OnZoneEnter -= HandleItemInteraction;
            _interactionZone.OnZoneEnter -= HandleEnemyInteraction;
        }

        private void HandleEnemyInteraction(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out EnemyModel enemyModel))
            {
                _playerAttackService.SetTarget(enemyModel.transform);
            }
        }

        private void HandleItemInteraction(Collider2D obj)
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