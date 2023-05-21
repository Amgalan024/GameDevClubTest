using UnityEngine;

namespace Enemy
{
    public class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private EnemyModel _enemyModel;
        [SerializeField] private Transform _visualizationRoot;
        [SerializeField] private Transform[] _colliderRoots;

        private void Awake()
        {
            _enemyModel.OnDeath += PlayDeathAnimation;
        }

        private void OnDestroy()
        {
            _enemyModel.OnDeath -= PlayDeathAnimation;
        }

        private void PlayDeathAnimation()
        {
            _visualizationRoot.Rotate(0, 0, 90);

            _enemyModel.OnDeath -= PlayDeathAnimation;

            foreach (var root in _colliderRoots)
            {
                root.gameObject.SetActive(false);
            }
        }
    }
}