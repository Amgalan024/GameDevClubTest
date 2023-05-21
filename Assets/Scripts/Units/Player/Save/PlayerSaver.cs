using System.Linq;
using Inventory;
using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Units.Player.Services
{
    public class PlayerSaver : BaseSaver
    {
        [SerializeField] private PlayerModel _playerModel;

        public override string Save()
        {
            var playerModelTransform = _playerModel.transform;

            var inventoryItems = _playerModel.UnitServiceProvider.GetService<InventoryService>().ItemsByIcons.Values
                .ToList();

            var playerSaveData = new PlayerSaveData(IdentifierHolder.AssetId, _playerModel.CurrentHealth,
                playerModelTransform.position, playerModelTransform.localScale, inventoryItems);
            
            var json = JsonConvert.SerializeObject(playerSaveData);

            return json;
        }

        public override void Load(string playerSaveData)
        {
        }
    }
}