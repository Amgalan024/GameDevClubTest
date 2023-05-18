using System;
using Item;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action OnClicked;

        [SerializeField] private Image _icon;
        [SerializeField] private Text _amount;

        public void SetInventoryItem(BaseItem item)
        {
            _icon.sprite = item.Icon;

            if (item.Amount > 1)
            {
                _amount.text = item.Amount.ToString();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke();
        }
    }
}