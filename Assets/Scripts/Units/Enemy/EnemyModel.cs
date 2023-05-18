using Item;
using Units;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel : BaseUnit
    {
        [SerializeField] private BaseItem _dropItem;
        [SerializeField] private float _speed;

        public BaseItem DropItem => _dropItem;
        public float Speed => _speed;
    }
}