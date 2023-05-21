using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Units.Player.Services;
using UnityEngine;

namespace Startup
{
    public class SaveLoadService : MonoBehaviour
    {
        [SerializeField] private AssetLoadList _assetLoadList;

        [SerializeField] private LevelContainer _levelContainer;

        private string SavePath => Application.persistentDataPath + "/Save.json";

        public void Save()
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

            Debug.Log("Save at " + SavePath);
        }

        public void Load()
        {
            var fileString = System.IO.File.ReadAllText(SavePath);
            var saveData = JsonConvert.DeserializeObject<List<string>>(fileString);
            Debug.Log("Loaded");
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                Save();
            }
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }
        }
    }
}