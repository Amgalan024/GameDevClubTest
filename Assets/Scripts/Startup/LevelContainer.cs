using System;
using System.Collections.Generic;
using System.Linq;
using Enemy;
using Item;
using UnityEngine;

namespace Startup
{
    public class LevelContainer : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private List<EnemyModel> _enemyModels;
        [SerializeField] private List<DropItem> _droppedItemsOnScene;
        [SerializeField] private List<BaseSaver> _objectToSave;

        public PlayerModel PlayerModel => _playerModel;
        public IEnumerable<EnemyModel> EnemyModels => _enemyModels.AsEnumerable();
        public IEnumerable<DropItem> DroppedItemsOnScene => _droppedItemsOnScene.AsEnumerable();
        public IEnumerable<BaseSaver> ObjectToSave => _objectToSave.AsEnumerable();

        private readonly Dictionary<EnemyModel, Action> _enemySubscriptions = new Dictionary<EnemyModel, Action>();
        private readonly Dictionary<DropItem, Action> _itemSubscriptions = new Dictionary<DropItem, Action>();
        private readonly Dictionary<BaseSaver, Action> _savableSubscriptions = new Dictionary<BaseSaver, Action>();

        public void SetPlayerModel(PlayerModel playerModel)
        {
            _playerModel = playerModel;
        }

        public void AddSavableObject(BaseSaver baseSaver)
        {
            _objectToSave.Add(baseSaver);

            Action subscription = () => RemoveSavable(baseSaver);

            _savableSubscriptions.Add(baseSaver, subscription);

            baseSaver.OnDeleted += subscription;
        }

        public void AddEnemyModels(EnemyModel enemyModel)
        {
            _enemyModels.Add(enemyModel);

            Action subscription = () => RemoveEnemyModel(enemyModel);

            _enemySubscriptions.Add(enemyModel, subscription);

            enemyModel.OnDeath += subscription;
        }

        public void AddDroppedItem(DropItem dropItem)
        {
            _droppedItemsOnScene.Add(dropItem);

            Action subscription = () => RemoveDroppedItem(dropItem);

            _itemSubscriptions.Add(dropItem, subscription);

            dropItem.OnPickedUp += subscription;
        }

        private void RemoveSavable(BaseSaver baseSaver)
        {
            _objectToSave.Remove(baseSaver);

            baseSaver.OnDeleted -= _savableSubscriptions[baseSaver];
        }

        private void RemoveEnemyModel(EnemyModel enemyModel)
        {
            _enemyModels.Remove(enemyModel);

            enemyModel.OnDeath -= _enemySubscriptions[enemyModel];
        }

        private void RemoveDroppedItem(DropItem dropItem)
        {
            _droppedItemsOnScene.Remove(dropItem);

            dropItem.OnPickedUp -= _itemSubscriptions[dropItem];
        }
    }
}