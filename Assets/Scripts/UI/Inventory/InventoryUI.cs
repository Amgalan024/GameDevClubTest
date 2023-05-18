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

        private void Awake()
        {
            _openButton.onClick.AddListener(Open);
            _closeButton.onClick.AddListener(Close);
        }

        private void OnDestroy()
        {
            _openButton.onClick.RemoveAllListeners();
            _closeButton.onClick.RemoveAllListeners();
        }

        private void Open()
        {
            _root.gameObject.SetActive(true);
        }

        private void Close()
        {
            _root.gameObject.SetActive(false);
        }
    }
}