using System;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerAttackController : MonoBehaviour
    {
        [SerializeField] private Button _shootButton;
        [SerializeField] private PlayerAttackService _playerAttackService;
        
        private void Awake()
        {
            _shootButton.onClick.AddListener(()=>_playerAttackService.Shoot());
        }

        private void OnDestroy()
        {
            _shootButton.onClick.RemoveAllListeners();
        }
    }
}