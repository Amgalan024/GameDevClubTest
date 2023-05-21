using System;
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
        [SerializeField] private EnemyAttackService _enemyAttackService;

        private void Awake()
        {
            _enemyModel.OnDeath += OnEnemyDeath;
            _aggroZone.OnZoneEnter += OnAggroZoneEntered;
            _hitBox.OnZoneEnter += OnHit;
            _attackRangeZone.OnZoneEnter += OnAttackRangeEntered;
            _attackRangeZone.OnZoneExit += OnAttackRangeExited;
        }

        private void OnDestroy()
        {
            OnEnemyDeath();
        }

        private void OnEnemyDeath()
        {
            _enemyModel.OnDeath -= OnEnemyDeath;
            _aggroZone.OnZoneEnter -= OnAggroZoneEntered;
            _hitBox.OnZoneEnter -= OnHit;
            _attackRangeZone.OnZoneEnter -= OnAttackRangeEntered;
            _attackRangeZone.OnZoneExit -= OnAttackRangeExited;
            
            _enemyAttackService.StopAttacking();
            _enemyMovementService.StopChasing();
        }

        private void OnAttackRangeEntered(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out PlayerModel playerModel))
            {
                _enemyAttackService.StartAttacking(playerModel);
            }
        }

        private void OnAttackRangeExited(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out PlayerModel playerModel))
            {
                _enemyAttackService.StopAttacking();
            }
        }

        private void OnHit(Collider2D obj)
        {
            var rb = obj.attachedRigidbody;

            if (rb != null && rb.TryGetComponent(out ProjectileModel projectileModel))
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
                _enemyMovementService.SetTarget(playerModel);
            }
        }
    }
}