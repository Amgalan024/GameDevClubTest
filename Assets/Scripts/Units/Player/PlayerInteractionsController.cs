﻿using System;
using Enemy;
using Inventory;
using Item;
using Units.Zones;
using UnityEngine;

namespace Player
{
    public class PlayerInteractionsController : MonoBehaviour
    {
        [SerializeField] private TriggerZone _interactionZone;
        [SerializeField] private InventoryService _inventoryService;
        [SerializeField] private PlayerAttackService _playerAttackService;

        private void Awake()
        {
            _interactionZone.OnZoneEnter += HandleItemInteraction;
            _interactionZone.OnZoneEnter += HandleEnemyInteraction;
        }

        private void HandleEnemyInteraction(Collider2D obj)
        {
            if (obj.TryGetComponent(out EnemyModel enemyModel))
            {
                _playerAttackService.SetTarget(enemyModel.transform);
            }
        }

        private void HandleItemInteraction(Collider2D obj)
        {
            if (obj.TryGetComponent(out BaseItem item))
            {
                _inventoryService.AddItem(item);
            }
        }
    }
}