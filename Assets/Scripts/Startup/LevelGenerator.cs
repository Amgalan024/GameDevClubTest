using System.Collections.Generic;
using System.Linq;
using Enemy;
using Item.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Startup
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Core")] [SerializeField] private AssetLoadList _assetLoadList;
        [SerializeField] private LevelContainer _levelContainer;

        [Header("Player Spawn Data")] [SerializeField]
        private PlayerModel _playerPrefab;

        [SerializeField] private Transform _playerSpawnPoint;

        [Header("Enemy Spawn Data")] [SerializeField]
        private EnemyModel _enemyPrefab;

        [SerializeField] private int _enemiesCount;
        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private Tilemap _obstaclesTileMap;

        [Header("Item Spawn Data")] [SerializeField]
        private ItemSpawnData[] _itemSpawnData;

        private List<Vector3Int> _freeTilesPositions;

        public void GenerateInitialLevel()
        {
            CreateEnemies();
            CreatePlayer();
            CreateItems();
        }

        public void GenerateLevelFromSave(List<string> loadData)
        {
            foreach (var json in loadData)
            {
                var data = JObject.Parse(json);

                var jToken = data.GetValue("AssetId");

                var assetId = jToken.Value<string>();

                var asset = _assetLoadList.Assets.First(a => a.GetComponent<IdentifierHolder>().AssetId == assetId);

                var instantiatedAsset = Instantiate(asset);

                var assetSaver = instantiatedAsset.GetComponent<BaseSaver>();

                assetSaver.Load(json);

                _levelContainer.AddSavableObject(assetSaver);
            }
        }

        private void CreateEnemies()
        {
            _freeTilesPositions = new List<Vector3Int>(_groundTilemap.cellBounds.x * _groundTilemap.cellBounds.y);

            foreach (var position in _groundTilemap.cellBounds.allPositionsWithin)
            {
                var tile = _obstaclesTileMap.GetTile(position);

                if (tile == null)
                {
                    _freeTilesPositions.Add(position);
                }
            }

            for (int i = 0; i < _enemiesCount; i++)
            {
                var randomIndex = Random.Range(0, _freeTilesPositions.Count);
                var randomPosition = _freeTilesPositions[randomIndex];

                var enemy = Instantiate(_enemyPrefab, randomPosition, Quaternion.identity);

                _levelContainer.AddEnemyModels(enemy);

                _freeTilesPositions.Remove(randomPosition);
            }
        }

        private void CreatePlayer()
        {
            var playerModel = Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);

            _levelContainer.SetPlayerModel(playerModel);
        }

        private void CreateItems()
        {
            foreach (var spawnData in _itemSpawnData)
            {
                var dropItem = Instantiate(spawnData.DropItemPrefab, spawnData.SpawnPoint);

                _levelContainer.AddDroppedItem(dropItem);
            }
        }
    }
}