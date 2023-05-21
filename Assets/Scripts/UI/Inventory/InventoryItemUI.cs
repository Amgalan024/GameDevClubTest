using System;
using Item;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryItemUI : MonoBehaviour, IPointerClickHandler
    {
        public event Action<InventoryItemUI> OnClicked;

        [SerializeField] private Image _icon;
        [SerializeField] private Sprite _emptySprite;
        [SerializeField] private TextMeshProUGUI _amount;

        public void SetInventoryItem(Sprite icon, int amount)
        {
            _icon.sprite = icon;

            SetAmount(amount);
        }

        public void SetEmpty()
        {
            _icon.sprite = _emptySprite;
            _amount.text = "";
        }

        public void SetAmount(int amount)
        {
            if (amount > 1)
            {
                _amount.text = amount.ToString();
            }
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnClicked?.Invoke(this);
        }
    }
}