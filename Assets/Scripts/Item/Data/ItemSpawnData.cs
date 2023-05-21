using System;
using UnityEngine;

namespace Item.Data
{
    [Serializable]
    public class ItemSpawnData
    {
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private DropItem _dropItemPrefab;

        public Transform SpawnPoint => _spawnPoint;
        public DropItem DropItemPrefab => _dropItemPrefab;
    }
}