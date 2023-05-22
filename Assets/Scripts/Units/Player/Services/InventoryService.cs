using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

namespace Inventory
{
    public class InventoryService : MonoBehaviour
    {
        public event Action<InventoryItem> OnItemAdded;
        public event Action<InventoryItem> OnItemRemoved;

        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private InventoryItemUI _inventoryItemPrefab;
        [SerializeField] private int _capacity;

        public Dictionary<InventoryItemUI, InventoryItem> ItemsByIcons { get; } =
            new Dictionary<InventoryItemUI, InventoryItem>();

        private readonly List<InventoryItemUI> _icons = new List<InventoryItemUI>();

        private readonly Dictionary<InventoryItem, Action<int>> _amountChangedSubscriptions =
            new Dictionary<InventoryItem, Action<int>>();

        private InventoryItemUI _activeIcon;

        private void Awake()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var icon = Instantiate(_inventoryItemPrefab, _inventoryUI.ItemsLayoutGroup.transform);
                icon.SetEmpty();
                _icons.Add(icon);
            }

            _inventoryUI.DeleteItemButton.onClick.AddListener(() =>
            {
                RemoveItem(_activeIcon);
                _inventoryUI.HideItemActionButtons();
            });

            _playerModel.OnDeath += OnPlayerDeath;
        }

        private void Start()
        {
            _inventoryUI.Close();
        }

        private void OnDestroy()
        {
            OnPlayerDeath();
        }

        private void SetIconActive(InventoryItemUI inventoryItemUI)
        {
            _activeIcon = inventoryItemUI;
            _inventoryUI.ShowItemActionButtons(inventoryItemUI);
        }

        public void AddItem(ItemBehaviour itemBehaviour, int amount)
        {
            var existingInventoryItem = GetInventoryItemByBehaviour(itemBehaviour);

            if (existingInventoryItem != null)
            {
                existingInventoryItem.TryChangeAmount(amount);

                OnItemAdded?.Invoke(existingInventoryItem);

                return;
            }

            var icon = GetFreeIcon();

            icon.SetInventoryItem(itemBehaviour.Icon, amount);

            icon.OnClicked += SetIconActive;

            var inventoryItem = new InventoryItem(itemBehaviour, amount);

            Action<int> subscription = amount => { OnItemAmountChanged(icon, amount); };

            inventoryItem.OnAmountChanged += subscription;

            _amountChangedSubscriptions.Add(inventoryItem, subscription);

            ItemsByIcons.Add(icon, inventoryItem);

            itemBehaviour.OnAdded(_playerModel, inventoryItem);

            OnItemAdded?.Invoke(inventoryItem);
        }

        public InventoryItem GetInventoryItemByBehaviour(ItemBehaviour itemBehaviour)
        {
            var itemByIcon = ItemsByIcons.FirstOrDefault(i => i.Value.ItemBehaviour == itemBehaviour);

            return itemByIcon.Value;
        }

        public void RemoveItem(InventoryItemUI itemIcon)
        {
            var inventoryItem = ItemsByIcons[itemIcon];
            inventoryItem.OnAmountChanged -= _amountChangedSubscriptions[ItemsByIcons[itemIcon]];
            inventoryItem.ResetAmount();

            itemIcon.OnClicked -= SetIconActive;
            itemIcon.SetEmpty();

            OnItemRemoved?.Invoke(inventoryItem);

            ItemsByIcons.Remove(itemIcon);
        }

        private void OnItemAmountChanged(InventoryItemUI itemIcon, int amount)
        {
            itemIcon.SetAmount(amount);

            if (amount <= 0)
            {
                RemoveItem(itemIcon);
            }
        }

        private void OnPlayerDeath()
        {
            foreach (var icon in _icons)
            {
                icon.OnClicked -= SetIconActive;
            }

            foreach (var itemByIcon in ItemsByIcons)
            {
                itemByIcon.Value.OnAmountChanged -= _amountChangedSubscriptions[itemByIcon.Value];
            }

            _inventoryUI.DeleteItemButton.onClick.RemoveAllListeners();

            _playerModel.OnDeath -= OnPlayerDeath;
        }

        private InventoryItemUI GetFreeIcon()
        {
            return _icons.First(i => ItemsByIcons.ContainsKey(i) == false);
        }
    }
}