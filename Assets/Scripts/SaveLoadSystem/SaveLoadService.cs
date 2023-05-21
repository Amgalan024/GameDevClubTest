using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Units.Player.Services;
using UnityEngine;

namespace Startup
{
    public class SaveLoadService : MonoBehaviour
    {
        [SerializeField] private LevelContainer _levelContainer;

        private string SavePath => Application.persistentDataPath + "/Save.json";

        public void SaveData()
        {
            var levelSaveData = new List<string>();

            var playerData = _levelContainer.PlayerModel.GetComponent<BaseSaver>().Save();
            levelSaveData.Add(playerData);

            foreach (var enemyModel in _levelContainer.EnemyModels)
            {
                var enemyData = enemyModel.GetComponent<BaseSaver>().Save();
                levelSaveData.Add(enemyData);
            }

            foreach (var dropItem in _levelContainer.DroppedItemsOnScene)
            {
                var itemData = dropItem.GetComponent<BaseSaver>().Save();
                levelSaveData.Add(itemData);
            }

            var json = JsonConvert.SerializeObject(levelSaveData.ToArray());

            System.IO.File.WriteAllText(SavePath, json);
        }

        public bool TryGetLoadData(out List<string> loadData)
        {
            if (System.IO.File.Exists(SavePath))
            {
                var fileString = System.IO.File.ReadAllText(SavePath);

                loadData = JsonConvert.DeserializeObject<List<string>>(fileString);

                return true;
            }

            loadData = null;

            return false;
        }

    }
}