using System;
using System.Linq;
using Gameplay;
using UnityEngine;

namespace Startup
{
    public class SaveController : MonoBehaviour
    {
        [SerializeField] private SaveLoadService _saveLoadService;
        [SerializeField] private ApplicationQuitHandler _applicationQuitHandler;
        [SerializeField] private LevelGenerator _levelGenerator;
        [SerializeField] private LevelContainer _levelContainer;

        private PlayerModel _playerModel;

        private void Awake()
        {
            _levelGenerator.OnLevelGenerated += FindPLayer;
            _applicationQuitHandler.OnQuit += _saveLoadService.SaveData;
        }

        private void OnDestroy()
        {
            _levelGenerator.OnLevelGenerated -= FindPLayer;
            _applicationQuitHandler.OnQuit -= _saveLoadService.SaveData;
            _playerModel.OnDeath -= OnPlayerDeath;
        }

        private void FindPLayer()
        {
            _playerModel = _levelContainer.ObjectToSave.First(o => o.GetComponent<PlayerModel>())
                .GetComponent<PlayerModel>();

            _playerModel.OnDeath += OnPlayerDeath;
        }

        private void OnPlayerDeath()
        {
            _applicationQuitHandler.OnQuit -= _saveLoadService.SaveData;
        }
    }
}