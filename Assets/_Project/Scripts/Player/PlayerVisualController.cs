using UnityEngine;

namespace EscapeUNIFRANZ.Player
{
    /// <summary>
    /// Converts movement state into horizontal facing and the minimum Animator state.
    /// </summary>
    [RequireComponent(typeof(SpriteRenderer), typeof(Animator))]
    public sealed class PlayerVisualController : MonoBehaviour
    {
        public const string IsWalkingParameter = "IsWalking";

        public enum HorizontalFacing
        {
            Left,
            Right
        }

        private static readonly int IsWalkingHash = Animator.StringToHash(IsWalkingParameter);

        [SerializeField] private PlayerMovement2D movement;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private Animator animator;
        [SerializeField] private HorizontalFacing initialFacing = HorizontalFacing.Right;
        [SerializeField] private bool sourceSpriteFacesRight = true;

        public HorizontalFacing CurrentFacing { get; private set; }

        private void Awake()
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponent<SpriteRenderer>();
            }

            if (animator == null)
            {
                animator = GetComponent<Animator>();
            }

            if (movement == null)
            {
                Debug.LogError(
                    $"{nameof(PlayerVisualController)} on '{name}' requires a {nameof(PlayerMovement2D)} reference.",
                    this);
            }

            CurrentFacing = initialFacing;
            ApplyFacing();
        }

        private void Update()
        {
            Vector2 input = movement != null ? movement.MovementInput : Vector2.zero;
            CurrentFacing = ResolveFacing(input, CurrentFacing);
            ApplyFacing();
            ApplyWalkingState(movement != null && movement.IsMoving);
        }

        private void OnDisable()
        {
            ApplyWalkingState(false);
        }

        public static HorizontalFacing ResolveFacing(
            Vector2 movementInput,
            HorizontalFacing previousFacing)
        {
            if (movementInput.x > 0f)
            {
                return HorizontalFacing.Right;
            }

            if (movementInput.x < 0f)
            {
                return HorizontalFacing.Left;
            }

            return previousFacing;
        }

        private void ApplyFacing()
        {
            if (spriteRenderer == null)
            {
                return;
            }

            bool facesOppositeToSource = sourceSpriteFacesRight
                ? CurrentFacing == HorizontalFacing.Left
                : CurrentFacing == HorizontalFacing.Right;

            spriteRenderer.flipX = facesOppositeToSource;
        }

        private void ApplyWalkingState(bool isWalking)
        {
            if (animator != null && animator.runtimeAnimatorController != null)
            {
                animator.SetBool(IsWalkingHash, isWalking);
            }
        }
    }
}
