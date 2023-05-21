using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Button _shootButton;
        [SerializeField] private PlayerAttackService _playerAttackService;

        private void Awake()
        {
            _shootButton.onClick.AddListener(() => _playerAttackService.Shoot());

            _playerModel.OnDeath += OnPlayerDeath;
        }

        private void OnDestroy()
        {
            OnPlayerDeath();
        }

        private void OnPlayerDeath()
        {
            _shootButton.onClick.RemoveAllListeners();

            _playerModel.OnDeath -= OnPlayerDeath;
        }
    }
}