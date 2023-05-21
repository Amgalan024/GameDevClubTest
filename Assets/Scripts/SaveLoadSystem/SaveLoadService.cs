using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
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

            foreach (var saver in _levelContainer.ObjectToSave)
            {
                var data = saver.Save();
                levelSaveData.Add(data);
            }

            var json = JsonConvert.SerializeObject(levelSaveData.ToArray());

            File.WriteAllText(SavePath, json);
            
            Debug.Log("SavedData");
        }

        public bool TryGetLoadData(out List<string> loadData)
        {
            if (File.Exists(SavePath))
            {
                var fileString = File.ReadAllText(SavePath);

                loadData = JsonConvert.DeserializeObject<List<string>>(fileString);

                return true;
            }

            loadData = null;

            return false;
        }

        public void ClearData()
        {
            File.Delete(SavePath);
        }
    }
}