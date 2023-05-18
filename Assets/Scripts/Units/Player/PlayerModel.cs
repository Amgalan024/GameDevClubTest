using Units;
using UnityEngine;

public class PlayerModel : BaseUnit
{
    [SerializeField] private float _speed;

    public float Speed => _speed;
}