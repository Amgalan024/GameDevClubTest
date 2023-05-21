using System;
using UnityEngine;

namespace Enemy
{
    public class EnemyDropController : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Transform _dropPoint;

        private void Awake()
        {
            _enemyModel.OnDeath += DropItem;
        }

        private void OnDestroy()
        {
            _enemyModel.OnDeath -= DropItem;
        }

        private void DropItem()
        {
            var droppedItem = Instantiate(_enemyModel.DropItem, _dropPoint.position, Quaternion.identity);

            _enemyModel.OnDeath -= DropItem;
        }
    }
}