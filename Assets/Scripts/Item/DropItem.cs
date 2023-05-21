using System;
using UnityEngine;

namespace Item
{
    public class DropItem : MonoBehaviour
    {
        public event Action OnPickedUp;

        [SerializeField] private int _amount;
        [SerializeField] private ItemBehaviour _itemBehaviourPrefab;

        public int Amount => _amount;
        public ItemBehaviour ItemBehaviourPrefab => _itemBehaviourPrefab;

        public void PickUp()
        {
            OnPickedUp?.Invoke();
            
            Destroy(gameObject);
        }

        private void OnValidate()
        {
            if (_amount <= 0)
            {
                _amount = 1;
            }
        }
    }
}