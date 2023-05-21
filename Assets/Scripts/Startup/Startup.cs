using System.Collections.Generic;
using UnityEngine;

namespace Startup
{
    public class Startup : MonoBehaviour
    {
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private SaveLoadService _saveLoadService;

        private void Start()
        {
            // if (_saveLoadService.TryGetLoadData(out List<string> data))
            // {
            //     _levelGenerator.GenerateLevelFromSave(data);
            // }
            // else
            // {
            //     _levelGenerator.GenerateInitialLevel();
            // }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                _saveLoadService.SaveData();
            }

            if (Input.GetKeyDown(KeyCode.L))
            {
                if (_saveLoadService.TryGetLoadData(out List<string> data))
                {
                    _levelGenerator.GenerateLevelFromSave(data);
                }
            }

            if (Input.GetKeyDown(KeyCode.D))
            {
                _levelGenerator.GenerateInitialLevel();
            }
        }
    }
}