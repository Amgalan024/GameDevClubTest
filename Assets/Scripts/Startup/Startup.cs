using System.Collections.Generic;
using UnityEngine;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private SaveLoadService _saveLoadService;
        [SerializeField] private bool _loadSaveFile;
        
        private void Start()
        {
            if (_loadSaveFile && _saveLoadService.TryGetLoadData(out List<string> data))
            {
                _levelGenerator.GenerateLevelFromSave(data);
            }
            else
            {
                _levelGenerator.GenerateDefaultLevel();
            }
        }
    }
}