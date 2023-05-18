using System.Collections;
using UnityEngine;

namespace Enemy
{
    public class EnemyMovement : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;

        private Transform _target;

        private Coroutine _chaseCoroutine;
        
        public void SetTarget(Transform target)
        {
            _target = target;
            StopCoroutine(_chaseCoroutine);
            _chaseCoroutine = StartCoroutine(ChaseTargetCoroutine());
        }

        public void StopChasing()
        {
            StopCoroutine(_chaseCoroutine);
        }

        private IEnumerator ChaseTargetCoroutine()
        {
            while (true)
            {
                var position = (Vector2) _enemyModel.transform.position;
                var targetPosition = (Vector2) _target.transform.position;
                var direction = (targetPosition - position).normalized;

                position = Vector2.Lerp(position, position + direction, Time.deltaTime * _enemyModel.Speed);

                _enemyModel.transform.position = position;

                yield return null;
            }
        }
    }
}