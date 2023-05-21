using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovementService : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Transform _target;

        private Coroutine _chaseCoroutine;

        public void SetTarget(Transform target)
        {
            _target = target;

            if (_chaseCoroutine != null)
            {
                StopCoroutine(_chaseCoroutine);
            }

            _chaseCoroutine = StartCoroutine(ChaseTargetCoroutine());
        }

        public void StopChasing()
        {
            if (_chaseCoroutine != null)
            {
                StopCoroutine(_chaseCoroutine);
            }
        }

        private IEnumerator ChaseTargetCoroutine()
        {
            while (_target != null)
            {
                var position = (Vector2) _enemyModel.transform.position;
                var targetPosition = (Vector2) _target.transform.position;
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