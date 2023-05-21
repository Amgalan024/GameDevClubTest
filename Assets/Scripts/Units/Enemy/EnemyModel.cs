using Item;
using Units;
using UnityEngine;

namespace Enemy
{
    public class EnemyModel : BaseUnit
    {
        [SerializeField] private DropItem _dropItem;
        [SerializeField] private float _speed;
        [SerializeField] private int _damage;
        [SerializeField] private UnitServiceProvider _unitServiceProvider;
        public DropItem DropItem => _dropItem;
        public float Speed => _speed;
        public int Damage => _damage;
        public UnitServiceProvider UnitServiceProvider => _unitServiceProvider;
    }
}