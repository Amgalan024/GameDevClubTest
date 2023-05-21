using System.Collections;
using UI.JoyStick;
using UnityEngine;

namespace Player
{
    public class PlayerMovementController : MonoBehaviour
    {
        [SerializeField] private JoyStickUI _joyStickUI;
        [SerializeField] private PlayerModel _playerModel;
        [SerializeField] private Rigidbody2D _rigidbody2D;

        private Coroutine _moveCoroutine;

        private void Awake()
        {
            _playerModel.OnDeath += OnPlayerDeath;
            _joyStickUI.OnMoveStarted += StartMoving;
            _joyStickUI.OnMoveEnded += StopMoving;
        }

        private void OnDestroy()
        {
            OnPlayerDeath();
        }

        private void OnPlayerDeath()
        {
            StopMoving();

            _playerModel.OnDeath -= OnPlayerDeath;
            _joyStickUI.OnMoveStarted -= StartMoving;
            _joyStickUI.OnMoveEnded -= StopMoving;
        }

        private void StartMoving()
        {
            StopMoving();

            _moveCoroutine = StartCoroutine(MovePlayerCoroutine());
        }

        private void StopMoving()
        {
            if (_moveCoroutine != null)
            {
                StopCoroutine(_moveCoroutine);
            }
        }

        private IEnumerator MovePlayerCoroutine()
        {
            while (true)
            {
                var position = (Vector2) _playerModel.transform.position;

                position = position + _joyStickUI.GetDirection() * Time.fixedDeltaTime * _playerModel.Speed;

                _rigidbody2D.MovePosition(position);

                yield return new WaitForFixedUpdate();
            }
        }
    }
}