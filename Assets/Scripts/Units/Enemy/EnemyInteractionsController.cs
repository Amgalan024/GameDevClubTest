using Projectile;
using Units.Zones;
using UnityEngine;

namespace Enemy
{
    public class EnemyInteractionsController : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private TriggerZone _aggroZone;
        [SerializeField] private TriggerZone _hitBox;
        [SerializeField] private EnemyMovement _enemyMovement;

        private void Awake()
        {
            _aggroZone.OnZoneEnter += OnAggroZoneEntered;
            _hitBox.OnZoneEnter += OnHit;
        }

        private void OnHit(Collider2D obj)
        {
            if (obj.TryGetComponent(out ProjectileModel projectileModel))
            {
                _enemyModel.ChangeHealth(-projectileModel.Damage);

                Destroy(projectileModel);
            }
        }

        private void OnAggroZoneEntered(Collider2D obj)
        {
            if (obj.TryGetComponent(out PlayerModel playerModel))
            {
                _enemyMovement.SetTarget(playerModel.transform);
            }
        }
    }
}