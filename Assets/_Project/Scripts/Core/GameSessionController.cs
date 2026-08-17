using System.Collections;
using EscapeUNIFRANZ.UI;
using EscapeUNIFRANZ.World;
using UnityEngine;

namespace EscapeUNIFRANZ.Core
{
    /// <summary>
    /// Owns the current GameState and starts a new session from Bootstrap.
    /// </summary>
    public sealed class GameSessionController : MonoBehaviour
    {
        [SerializeField] private SceneFlowController sceneFlow;
        [SerializeField] private ObjectiveController objectiveController;
        [SerializeField] private string initialZoneId = "hall";
        [SerializeField] private string initialSpawnId = "from_bootstrap";
        [SerializeField] private bool startNewGameOnStart = true;

        private GameState state;

        public GameState State => state;

        private void Awake()
        {
            StartNewGame();
        }

        private IEnumerator Start()
        {
            if (!startNewGameOnStart)
            {
                yield break;
            }

            yield return null;
            sceneFlow.GoToZone(initialZoneId, initialSpawnId);
        }

        public void StartNewGame()
        {
            state = new GameState();
            objectiveController?.Bind(state);
        }
    }
}
