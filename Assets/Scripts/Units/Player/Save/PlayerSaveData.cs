using System;
using System.Collections.Generic;
using Item;
using Startup;
using UnityEngine;
using Utils.CustomSerializables;

namespace Units.Player.Services
{
    [Serializable]
    public class PlayerSaveData : BaseSaveData
    {
        public int HealthPoints;
        public Vector2Serializable Position;
        public Vector2Serializable Scale;
        public Dictionary<string, int> Items;

        public PlayerSaveData(string assetId, int healthPoints, Vector2 position, Vector2 scale,
            List<InventoryItem> items) : base(assetId)
        {
            HealthPoints = healthPoints;
            Position = new Vector2Serializable(position);
            Scale = new Vector2Serializable(scale);

            Items = new Dictionary<string, int>(items.Count);

            foreach (var item in items)
            {
                Items.Add(item.ItemBehaviour.GetComponent<IdentifierHolder>().AssetId, item.Amount);
            }
        }
    }
}