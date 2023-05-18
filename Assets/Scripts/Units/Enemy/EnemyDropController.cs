using UnityEngine;

namespace Enemy
{
    public class EnemyDropController : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;

        private void Awake()
        {
            _enemyModel.OnDeath += DropItem;
        }

        private void DropItem()
        {
            var droppedItem = Instantiate(_enemyModel.DropItem, transform.position, Quaternion.identity);
        }
    }
}