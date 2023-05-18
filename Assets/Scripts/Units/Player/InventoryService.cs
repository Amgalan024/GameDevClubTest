using System;
using System.Collections.Generic;
using Item;
using UnityEngine;

namespace Inventory
{
    public class InventoryService : MonoBehaviour
    {
        public event Action<BaseItem> OnItemAdded;

        [SerializeField] private InventoryUI _inventoryUI;
        [SerializeField] private InventoryItemUI _inventoryItemPrefab;
        [SerializeField] private int _capacity;

        private readonly List<BaseItem> _items = new List<BaseItem>();

        //При подписке на ивент добавления итема, подписать на кнопку удаления - удаление итема
        //(при клике на итем с кнопки удаления удаляется старая подписка и добавляется новая подписка на удаление данного итема)


        public void AddItem(BaseItem baseItem)
        {
            _items.Add(baseItem);
            OnItemAdded?.Invoke(baseItem);
        }

        public void RemoveItem(BaseItem baseItem)
        {
            _items.Remove(baseItem);
        }
    }
}