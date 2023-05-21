using Startup;

namespace Item.Save
{
    public class InventoryItemSaveData : BaseSaveData
    {
        public int Amount;

        public InventoryItemSaveData(string assetId, int amount) : base(assetId)
        {
            Amount = amount;
        }
    }
}