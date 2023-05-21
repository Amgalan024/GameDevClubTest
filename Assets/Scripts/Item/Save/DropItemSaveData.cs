using Startup;
using UnityEngine;
using Utils.CustomSerializables;

namespace Item.Save
{
    public class DropItemSaveData : BaseSaveData
    {
        public Vector2Serializable Position;

        public DropItemSaveData(string assetId, Vector2 position) : base(assetId)
        {
            Position = new Vector2Serializable(position);
        }
    }
}