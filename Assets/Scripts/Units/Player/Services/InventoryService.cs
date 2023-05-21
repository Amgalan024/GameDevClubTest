using System.Collections.Generic;
using System.Linq;
using Item;
using UnityEngine;

namespace Inventory
{
    public class InventoryService : MonoBehaviour
    {
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

                icon.OnClicked += SetIconActive;
                icon.OnClicked += _inventoryUI.ShowItemActionButtons;
            }

            _inventoryUI.DeleteItemButton.onClick.AddListener(() => { RemoveItem(_activeIcon); });

            _inventoryUI.Close();
        }

        private void SetIconActive(InventoryItemUI inventoryItemUI)
        {
            _activeIcon = inventoryItemUI;
        }

        private void OnDestroy()
        {
            _inventoryUI.DeleteItemButton.onClick.RemoveAllListeners();
        }

        public void AddItem(ItemBehaviour itemBehaviour, int amount)
        {
            var icon = GetFreeIcon();

            icon.SetInventoryItem(itemBehaviour.Icon, amount);

            var inventoryItem = new InventoryItem(itemBehaviour, amount);

            inventoryItem.OnAmountChanged += icon.SetAmount;

            ItemsByIcons.Add(icon, inventoryItem);

            itemBehaviour.OnAdded(_playerModel);
        }

        public void RemoveItem(InventoryItemUI itemIcon)
        {
            itemIcon.SetEmpty();
            ItemsByIcons.Remove(itemIcon);
        }

        public void RemoveItemAmount(ItemBehaviour itemBehaviour, int amount)
        {
            var itemByIcon = ItemsByIcons.FirstOrDefault(i => i.Value.ItemBehaviour == itemBehaviour);

            itemByIcon.Value.ChangeAmount(-amount);

            if (itemByIcon.Value.Amount <= 0)
            {
                itemByIcon.Key.SetEmpty();
                ItemsByIcons.Remove(itemByIcon.Key);
            }
        }

        private InventoryItemUI GetFreeIcon()
        {
            return _icons.First(i => ItemsByIcons.ContainsKey(i) == false);
        }
    }
}