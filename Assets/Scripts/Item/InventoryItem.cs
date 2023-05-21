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

        public bool TryChangeAmount(int value)
        {
            var startAmount = Amount;
            Amount += value;

            if (Amount < 0)
            {
                Amount = startAmount;
                return false;
            }

            OnAmountChanged?.Invoke(Amount);

            return true;
        }
    }
}