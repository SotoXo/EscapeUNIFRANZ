using EscapeUNIFRANZ.Core;
using EscapeUNIFRANZ.Interaction;
using UnityEngine;

namespace EscapeUNIFRANZ.World
{
    /// <summary>
    /// Reusable contextual exit configured with stable destination IDs.
    /// </summary>
    public sealed class ZoneExitInteractable : InteractableBehaviour
    {
        [SerializeField] private string targetZoneId;
        [SerializeField] private string targetSpawnId;

        private SceneFlowController sceneFlow;

        public void Bind(SceneFlowController controller)
        {
            sceneFlow = controller;
        }

        protected override void PerformInteraction()
        {
            if (sceneFlow == null)
            {
                Debug.LogError($"{nameof(ZoneExitInteractable)} '{name}' is not bound to SceneFlow.", this);
                return;
            }

            sceneFlow.GoToZone(targetZoneId, targetSpawnId);
        }
    }
}
