using System.Collections.Generic;
using System.Linq;
using Enemy;
using Tile;
using UnityEngine;

namespace Startup
{
    public class LevelGenerator : MonoBehaviour
    {
        [SerializeField] private LevelContainer _levelContainer;
        [SerializeField] private EnemyModel _enemyPrefab;
        [SerializeField] private PlayerModel _playerPrefab;
        [SerializeField] private List<TileModel> _map;
        [SerializeField] private int _enemiesCount;
        [SerializeField] private TileModel _playerSpawnPoint;

        public void CreateEnemies()
        {
            var freeTiles = GetFreeTiles();

            for (int i = 0; i < _enemiesCount; i++)
            {
                var randomTileIndex = Random.Range(0, freeTiles.Count);
                var freeTile = freeTiles[randomTileIndex];

                var enemy = Instantiate(_enemyPrefab, freeTile.transform.position,
                    Quaternion.identity);

                _levelContainer.EnemyModels.Add(enemy);

                freeTiles.Remove(freeTile);
            }
        }

        public void CreatePlayer()
        {
            _levelContainer.PlayerModel = Instantiate(_playerPrefab, _playerSpawnPoint.transform.position,
                Quaternion.identity);
        }

        private List<TileModel> GetFreeTiles()
        {
            var freeTiles = _map.Where(a => !a.IsObstacle).ToList(); //сделать класс тайл

            return freeTiles;
        }
    }
}