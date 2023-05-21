using UnityEngine;

namespace Item
{
    public abstract class ItemBehaviour : MonoBehaviour
    {
        [SerializeField] private Sprite _icon;
        public Sprite Icon => _icon;

        public abstract void Use(PlayerModel playerModel, InventoryItem correspondedInventoryItem);

        public virtual void OnAdded(PlayerModel playerModel, InventoryItem correspondedInventoryItem)
        {
        }

        public virtual void OnRemoved(PlayerModel playerModel)
        {
        }
    }
}