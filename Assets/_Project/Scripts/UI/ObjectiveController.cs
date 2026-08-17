using EscapeUNIFRANZ.Core;
using UnityEngine;

namespace EscapeUNIFRANZ.UI
{
    /// <summary>
    /// Owns the single current objective command and updates its view.
    /// </summary>
    public sealed class ObjectiveController : MonoBehaviour
    {
        [SerializeField] private ObjectiveView view;

        private GameState state;

        public void Bind(GameState gameState)
        {
            state = gameState;
            view?.Hide();
        }

        public void SetCurrent(string objectiveId, string text)
        {
            if (state == null)
            {
                Debug.LogError("ObjectiveController has not been bound to a GameState.", this);
                return;
            }

            state.SetCurrentObjective(objectiveId);
            view?.Show(text);
        }
    }
}
