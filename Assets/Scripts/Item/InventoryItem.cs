using System;

namespace Item
{
    public class InventoryItem
    {
        public event Action<int> OnAmountChanged; 
        public ItemBehaviour ItemBehaviour { get; }
        public int Amount { get; private set; }

        public InventoryItem(ItemBehaviour itemBehaviour, int amount)
        {
            ItemBehaviour = itemBehaviour;
            Amount = amount;
        }

        public InventoryItem(DropItem dropItem)
        {
            ItemBehaviour = dropItem.ItemBehaviourPrefab;
            Amount = dropItem.Amount;
        }

        public void ChangeAmount(int value)
        {
            Amount += value;
            OnAmountChanged?.Invoke(Amount);
        }
    }
}