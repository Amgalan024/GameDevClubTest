using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Item.Save
{
    public class DropItemSaver : BaseSaver
    {
        [SerializeField] private DropItem _dropItem;

        public override string Save()
        {
            var saveData = new DropItemSaveData(IdentifierHolder.AssetId, _dropItem.transform.position);

            var json = JsonConvert.SerializeObject(saveData);

            return json;
        }

        public override void Load(string saveData)
        {
        }
    }
}