using System.Collections;
using Units;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovementService : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private BaseUnit _targetUnit;

        private Coroutine _chaseCoroutine;

        public void SetTarget(BaseUnit target)
        {
            StopChasing();
            ResetTarget();

            _targetUnit = target;
            _targetUnit.OnDeath += StopChasing;
            _chaseCoroutine = StartCoroutine(ChaseTargetCoroutine());
        }

        public void StopChasing()
        {
            if (_chaseCoroutine != null)
            {
                StopCoroutine(_chaseCoroutine);
            }
        }

        private void ResetTarget()
        {
            if (_targetUnit != null)
            {
                _targetUnit.OnDeath -= StopChasing;
                _targetUnit = null;
            }
        }

        private IEnumerator ChaseTargetCoroutine()
        {
            while (_targetUnit != null)
            {
                var position = (Vector2) _enemyModel.transform.position;
                var targetPosition = (Vector2) _targetUnit.transform.position;
                if (Mathf.Abs(position.magnitude - targetPosition.magnitude) > 0.25f)
                {
                    var direction = (targetPosition - position).normalized;

                    position = position + direction * Time.fixedDeltaTime * _enemyModel.Speed;

                    _rigidbody2D.MovePosition(position);

                    var scale = _enemyModel.transform.localScale;

                    if (position.x > targetPosition.x)
                    {
                        scale.x = -1;
                    }
                    else
                    {
                        scale.x = 1;
                    }

                    _enemyModel.transform.localScale = scale;
                }

                yield return new WaitForFixedUpdate();
            }
        }
    }
}