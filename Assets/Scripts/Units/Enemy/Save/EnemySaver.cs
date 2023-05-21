using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Enemy.Save
{
    public class EnemySaver : BaseSaver
    {
        [SerializeField] private EnemyModel _enemyModel;

        public override string Save()
        {
            var enemySaveData = new EnemySaveData(IdentifierHolder.AssetId, _enemyModel.CurrentHealth,
                _enemyModel.transform.position, _enemyModel.transform.localScale);

            var json = JsonConvert.SerializeObject(enemySaveData);

            return json;
        }

        public override void Load(string saveData)
        {
        }
    }
}