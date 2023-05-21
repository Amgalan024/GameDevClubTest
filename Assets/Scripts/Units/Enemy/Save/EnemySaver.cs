using Newtonsoft.Json;
using Startup;
using UnityEngine;

namespace Enemy.Save
{
    public class EnemySaver : BaseSaver
    {
        [SerializeField] private EnemyModel _enemyModel;

        private void Awake()
        {
            _enemyModel.OnDeath += InvokeOnDeleted;
        }

        private void OnDestroy()
        {
            _enemyModel.OnDeath -= InvokeOnDeleted;
        }

        public override string Save()
        {
            var enemySaveData = new EnemySaveData(IdentifierHolder.AssetId, _enemyModel.CurrentHealth,
                _enemyModel.transform.position, _enemyModel.transform.localScale);

            var json = JsonConvert.SerializeObject(enemySaveData);

            return json;
        }

        public override void Load(string json)
        {
            var enemySaveData = JsonConvert.DeserializeObject<EnemySaveData>(json);

            _enemyModel.transform.position = enemySaveData.Position.ToVector();
            _enemyModel.transform.localScale = enemySaveData.Scale.ToVector();

            _enemyModel.SetHealth(enemySaveData.HealthPoints);
        }
    }
}