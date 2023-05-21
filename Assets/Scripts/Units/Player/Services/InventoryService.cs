using System;
using System.Collections.Generic;
using System.Linq;
using Item;
using Unity.VisualScripting;
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
        }

        public void AddItem(ItemBehaviour itemBehaviour, int amount)
        {
            var itemByIcon = ItemsByIcons.FirstOrDefault(i => i.Value.ItemBehaviour == itemBehaviour);

            if (itemByIcon.Key != null)
            {
                itemByIcon.Value.TryChangeAmount(amount);

                OnItemAdded?.Invoke(itemByIcon.Value);

                return;
            }

            var icon = GetFreeIcon();

            icon.SetInventoryItem(itemBehaviour.Icon, amount);

            icon.OnClicked += SetIconActive;
            icon.OnClicked += _inventoryUI.ShowItemActionButtons;

            var inventoryItem = new InventoryItem(itemBehaviour, amount);

            inventoryItem.OnAmountChanged += icon.SetAmount;

            ItemsByIcons.Add(icon, inventoryItem);

            itemBehaviour.OnAdded(_playerModel, inventoryItem);

            OnItemAdded?.Invoke(inventoryItem);
        }

        public InventoryItem GetItemByBehaviour(ItemBehaviour itemBehaviour)
        {
            var itemByIcon = ItemsByIcons.FirstOrDefault(i => i.Value.ItemBehaviour == itemBehaviour);

            return itemByIcon.Value;
        }

        public bool TryRemoveItemAmount(ItemBehaviour itemBehaviour, int amount)
        {
            var itemByIcon = ItemsByIcons.FirstOrDefault(i => i.Value.ItemBehaviour == itemBehaviour);

            if (itemByIcon.Key != null && itemByIcon.Value.TryChangeAmount(-amount))
            {
                if (itemByIcon.Value.Amount <= 0)
                {
                    RemoveItem(itemByIcon.Key);
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        public void RemoveItem(InventoryItemUI itemIcon)
        {
            ItemsByIcons[itemIcon].ResetAmount();
            ItemsByIcons[itemIcon].OnAmountChanged -= itemIcon.SetAmount;

            itemIcon.OnClicked -= SetIconActive;
            itemIcon.OnClicked -= _inventoryUI.ShowItemActionButtons;

            itemIcon.SetEmpty();

            OnItemRemoved?.Invoke(ItemsByIcons[itemIcon]);

            ItemsByIcons.Remove(itemIcon);
        }

        private void OnPlayerDeath()
        {
            foreach (var icon in _icons)
            {
                icon.OnClicked -= SetIconActive;
                icon.OnClicked -= _inventoryUI.ShowItemActionButtons;
            }

            foreach (var itemByIcon in ItemsByIcons)
            {
                itemByIcon.Value.OnAmountChanged -= itemByIcon.Key.SetAmount;
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