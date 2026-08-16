using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace EscapeUNIFRANZ.Input
{
    /// <summary>
    /// Reads the configured gameplay actions and exposes player intent as state or events.
    /// </summary>
    public sealed class PlayerInputReader : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Assign InputSystem_Actions/Player/Move (Value, Vector2).")]
        private InputActionReference moveAction;

        [SerializeField]
        [Tooltip("Assign InputSystem_Actions/Player/Interact (Button, E).")]
        private InputActionReference interactAction;

        private InputAction subscribedMoveAction;
        private InputAction subscribedInteractAction;
        private bool enabledMoveActionLocally;
        private bool enabledInteractActionLocally;

        public Vector2 MoveInput { get; private set; }
        public event Action InteractPressed;

        private void OnEnable()
        {
            if (moveAction == null || moveAction.action == null ||
                interactAction == null || interactAction.action == null)
            {
                Debug.LogError(
                    $"{nameof(PlayerInputReader)} on '{name}' requires Move and Interact InputActionReferences.",
                    this);
                enabled = false;
                return;
            }

            subscribedMoveAction = moveAction.action;
            subscribedInteractAction = interactAction.action;

            subscribedMoveAction.performed += OnMoveChanged;
            subscribedMoveAction.canceled += OnMoveChanged;
            subscribedInteractAction.started += OnInteractStarted;

            enabledMoveActionLocally = !subscribedMoveAction.enabled;
            if (enabledMoveActionLocally)
            {
                subscribedMoveAction.Enable();
            }

            enabledInteractActionLocally = !subscribedInteractAction.enabled;
            if (enabledInteractActionLocally)
            {
                subscribedInteractAction.Enable();
            }

            MoveInput = subscribedMoveAction.ReadValue<Vector2>();
        }

        private void OnDisable()
        {
            MoveInput = Vector2.zero;

            if (subscribedMoveAction != null)
            {
                subscribedMoveAction.performed -= OnMoveChanged;
                subscribedMoveAction.canceled -= OnMoveChanged;

                if (enabledMoveActionLocally)
                {
                    subscribedMoveAction.Disable();
                }
            }

            if (subscribedInteractAction != null)
            {
                subscribedInteractAction.started -= OnInteractStarted;

                if (enabledInteractActionLocally)
                {
                    subscribedInteractAction.Disable();
                }
            }

            subscribedMoveAction = null;
            subscribedInteractAction = null;
            enabledMoveActionLocally = false;
            enabledInteractActionLocally = false;
        }

        private void OnMoveChanged(InputAction.CallbackContext context)
        {
            MoveInput = context.ReadValue<Vector2>();
        }

        private void OnInteractStarted(InputAction.CallbackContext context)
        {
            if (context.control is KeyControl key && key.keyCode == Key.E)
            {
                InteractPressed?.Invoke();
            }
        }
    }
}
