using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Transform _visualizationRoot;
        [SerializeField] private Transform[] _colliderRoots;

        private void Awake()
        {
            _playerModel.OnDeath += PlayDeathAnimation;
        }

        private void OnDestroy()
        {
            _playerModel.OnDeath -= PlayDeathAnimation;
        }

        private void PlayDeathAnimation()
        {
            _visualizationRoot.Rotate(0, 0, 90);

            _playerModel.OnDeath -= PlayDeathAnimation;

            foreach (var root in _colliderRoots)
            {
                root.gameObject.SetActive(false);
            }
        }
    }
}