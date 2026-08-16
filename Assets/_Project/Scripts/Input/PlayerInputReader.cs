using UnityEngine;
using UnityEngine.InputSystem;

namespace EscapeUNIFRANZ.Input
{
    /// <summary>
    /// Reads the configured movement action and exposes the current player intent.
    /// </summary>
    public sealed class PlayerInputReader : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Assign InputSystem_Actions/Player/Move (Value, Vector2).")]
        private InputActionReference moveAction;

        private InputAction subscribedAction;
        private bool enabledActionLocally;

        public Vector2 MoveInput { get; private set; }

        private void OnEnable()
        {
            if (moveAction == null || moveAction.action == null)
            {
                Debug.LogError(
                    $"{nameof(PlayerInputReader)} on '{name}' requires a Move InputActionReference.",
                    this);
                enabled = false;
                return;
            }

            subscribedAction = moveAction.action;
            subscribedAction.performed += OnMoveChanged;
            subscribedAction.canceled += OnMoveChanged;

            enabledActionLocally = !subscribedAction.enabled;
            if (enabledActionLocally)
            {
                subscribedAction.Enable();
            }

            MoveInput = subscribedAction.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            MoveInput = Vector2.zero;

            if (subscribedAction == null)
            {
                return;
            }

            subscribedAction.performed -= OnMoveChanged;
            subscribedAction.canceled -= OnMoveChanged;

            if (enabledActionLocally)
            {
                subscribedAction.Disable();
            }

            subscribedAction = null;
            enabledActionLocally = false;
        }

        private void OnMoveChanged(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }
    }
}
