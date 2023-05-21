using System;
using System.Linq;
using Inventory;
using Item;
using Item.Save;
using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Units.Player.Services
{
    public class PlayerSaver : BaseSaver
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private AssetLoadList _assetLoadList;

        private void Awake()
        {
            _playerModel.OnDeath += InvokeOnDeleted;
        }

        private void OnDestroy()
        {
            _playerModel.OnDeath -= InvokeOnDeleted;
        }

        public override string Save()
        {
            var playerModelTransform = _playerModel.transform;

            var inventoryItems = _playerModel.UnitServiceProvider.GetService<InventoryService>().ItemsByIcons.Values
                .ToList();

            var itemsArray = new InventoryItemSaveData[inventoryItems.Count];

            for (int i = 0; i < itemsArray.Length; i++)
            {
                var dropItemsAssets = _assetLoadList.Assets.Where(a => a.GetComponent<DropItem>());

                foreach (var dropItemsAsset in dropItemsAssets)
                {
                    var behaviourFromDrop = dropItemsAsset.GetComponent<DropItem>().ItemBehaviourPrefab;

                    if (behaviourFromDrop == inventoryItems[i].ItemBehaviour)
                    {
                        itemsArray[i] =
                            new InventoryItemSaveData(dropItemsAsset.GetComponent<IdentifierHolder>().AssetId,
                                inventoryItems[i].Amount);
                    }
                }
            }

            var playerSaveData = new PlayerSaveData(IdentifierHolder.AssetId, _playerModel.CurrentHealth,
                playerModelTransform.position, playerModelTransform.localScale, itemsArray);

            var json = JsonConvert.SerializeObject(playerSaveData);

            return json;
        }

        public override void Load(string json)
        {
            var playerSaveData = JsonConvert.DeserializeObject<PlayerSaveData>(json);

            _playerModel.transform.position = playerSaveData.Position.ToVector();
            _playerModel.transform.localScale = playerSaveData.Scale.ToVector();

            _playerModel.SetHealth(playerSaveData.HealthPoints);

            var inventoryService = _playerModel.UnitServiceProvider.GetService<InventoryService>();

            foreach (var item in playerSaveData.ItemsArray)
            {
                var itemBehaviour =
                    _assetLoadList.Assets.First(a => a.GetComponent<IdentifierHolder>().AssetId == item.AssetId)
                        .GetComponent<DropItem>().ItemBehaviourPrefab;

                var amount = item.Amount;

                inventoryService.AddItem(itemBehaviour, amount);
            }
        }
    }
}