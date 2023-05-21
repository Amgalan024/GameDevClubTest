using Units;
using UnityEngine;

public class PlayerModel : BaseUnit
{
    [SerializeField] private float _speed;
    [SerializeField] private UnitServiceProvider _unitServiceProvider;
    
    public float Speed => _speed;
    public UnitServiceProvider UnitServiceProvider => _unitServiceProvider;
}