using System;
using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Item.Save
{
    public class DropItemSaver : BaseSaver
    {
        [SerializeField] private DropItem _dropItem;

        private void Awake()
        {
            _dropItem.OnPickedUp += InvokeOnDeleted;
        }

        private void OnDestroy()
        {
            _dropItem.OnPickedUp -= InvokeOnDeleted;
        }

        public override string Save()
        {
            var saveData = new DropItemSaveData(IdentifierHolder.AssetId, _dropItem.transform.position);

            var json = JsonConvert.SerializeObject(saveData);

            return json;
        }

        public override void Load(string json)
        {
            var dropItemSaveData = JsonConvert.DeserializeObject<DropItemSaveData>(json);

            _dropItem.transform.position = dropItemSaveData.Position.ToVector();
        }
    }
}