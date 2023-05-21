using System;
using System.Collections;
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

        public void StartAttacking(PlayerModel playerModel)
        {
            StopAttacking();

            _attackCoroutine = StartCoroutine(AttackCoroutine(playerModel));
        }

        public void StopAttacking()
        {
            if (_attackCoroutine != null)
            {
                StopCoroutine(_attackCoroutine);
            }
        }

        private IEnumerator AttackCoroutine(PlayerModel playerModel)
        {
            while (playerModel != null)
            {
                playerModel.ChangeHealth(-_enemyModel.Damage);
                _animator.SetTrigger(AttackTrigger);

                yield return new WaitForSeconds(_interval);
            }
        }
    }
}