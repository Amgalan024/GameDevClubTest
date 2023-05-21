using System;
using UnityEngine;
using UnityEngine.UI;

namespace Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform _root;
        [SerializeField] private LayoutGroup _itemsLayoutGroup;

        [SerializeField] private Button _openButton;
        [SerializeField] private Button _closeButton;

        [SerializeField] private Button _deleteItemButton;

        public Button DeleteItemButton => _deleteItemButton;
        public LayoutGroup ItemsLayoutGroup => _itemsLayoutGroup;

        private void Awake()
        {
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
            
            _deleteItemButton.gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        public void ShowItemActionButtons(InventoryItemUI itemUI)
        {
            _deleteItemButton.gameObject.SetActive(true);

            _deleteItemButton.transform.position = itemUI.transform.position;
        }

        public void HideItemActionButtons()
        {
            _deleteItemButton.gameObject.SetActive(false);
        }

        public void Open()
        {
            _root.gameObject.SetActive(true);
        }

        public void Close()
        {
            _root.gameObject.SetActive(false);
        }
    }
}