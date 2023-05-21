using System.Collections.Generic;
using Enemy;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Startup
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelContainer _levelContainer;
        [SerializeField] private EnemyModel _enemyPrefab;
        [SerializeField] private PlayerModel _playerPrefab;

        [SerializeField] private Tilemap _groundTilemap;
        [SerializeField] private Tilemap _obstaclesTileMap;

        [SerializeField] private int _enemiesCount;
        [SerializeField] private Transform _playerSpawnPoint;

        private List<Vector3Int> _freeTilesPositions;

        private void Awake()
        {
            CreateEnemies();
            CreatePlayer();
        }

        public void CreateEnemies()
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

        public void CreatePlayer()
        {
            var playerModel = Instantiate(_playerPrefab, _playerSpawnPoint.transform.position, Quaternion.identity);

            _levelContainer.SetPlayerModel(playerModel);
        }
    }
}