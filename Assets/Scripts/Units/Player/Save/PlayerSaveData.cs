using System;
using Item.Save;
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
        public InventoryItemSaveData[] ItemsArray;

        public PlayerSaveData(string assetId, int healthPoints, Vector2 position, Vector2 scale,
            InventoryItemSaveData[] items) : base(assetId)
        {
            HealthPoints = healthPoints;
            Position = new Vector2Serializable(position);
            Scale = new Vector2Serializable(scale);

            ItemsArray = items;
        }
    }


}