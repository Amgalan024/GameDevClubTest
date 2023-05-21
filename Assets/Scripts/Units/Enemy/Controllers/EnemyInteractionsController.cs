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
        [SerializeField] private TriggerZone _attackRangeZone;
        [SerializeField] private EnemyMovementService _enemyMovementService;

        private void Awake()
        {
            _aggroZone.OnZoneEnter += OnAggroZoneEntered;
            _hitBox.OnZoneEnter += OnHit;
            _attackRangeZone.OnZoneEnter += OnAttackRangeEntered;
        }

        private void OnAttackRangeEntered(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out PlayerModel playerModel))
            {
                playerModel.ChangeHealth(-_enemyModel.Damage);
            }
        }

        private void OnHit(Collider2D obj)
        {
            if (obj.TryGetComponent(out ProjectileModel projectileModel))
            {
                _enemyModel.ChangeHealth(-projectileModel.Damage);

                Destroy(projectileModel.gameObject);
            }
        }

        private void OnAggroZoneEntered(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out PlayerModel playerModel))
            {
                _enemyMovementService.SetTarget(playerModel.transform);
            }
        }
    }
}