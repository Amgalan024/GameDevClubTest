using System.Collections;
using Units;
using UnityEngine;

namespace Enemy
{
    public class EnemyAttackService : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _interval;

        private static readonly int AttackTrigger = Animator.StringToHash("Attack");

        private Coroutine _attackCoroutine;

        private BaseUnit _targetUnit;

        public void StartAttacking(BaseUnit unit)
        {
            StopAttacking();
            ResetTarget();
            _targetUnit = unit;
            _targetUnit.OnDeath += StopAttacking;
            _attackCoroutine = StartCoroutine(AttackCoroutine());
        }

        public void StopAttacking()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }

        private void ResetTarget()
        {
            if (_targetUnit != null)
            {
                _targetUnit.OnDeath -= StopAttacking;
                _targetUnit = null;
            }
        }

        private IEnumerator AttackCoroutine()
        {
            while (true)
            {
                _targetUnit.ChangeHealth(-_enemyModel.Damage);
                _animator.SetTrigger(AttackTrigger);

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}