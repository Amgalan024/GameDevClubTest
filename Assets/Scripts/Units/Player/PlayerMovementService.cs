using UI.JoyStick;
using UnityEngine;

namespace Player
{
    public class PlayerMovementService : MonoBehaviour
    {
        [SerializeField] private JoyStickUI _joyStickUI;
        [SerializeField] private PlayerModel _playerModel;

        private void Awake()
        {
            _joyStickUI.OnMoved += MovePlayer;
        }

        private void OnDestroy()
        {
            _joyStickUI.OnMoved -= MovePlayer;
        }

        private void MovePlayer()
        {
            var position = (Vector2) _playerModel.transform.position;

            position = Vector2.Lerp(position, position + _joyStickUI.GetDirection(), Time.deltaTime * _playerModel.Speed);

            _playerModel.transform.position = position;
        }
    }
}