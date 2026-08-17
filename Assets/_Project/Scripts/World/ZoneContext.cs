using System.Collections.Generic;
using EscapeUNIFRANZ.Core;
using EscapeUNIFRANZ.UI;
using UnityEngine;

namespace EscapeUNIFRANZ.World
{
    /// <summary>
    /// Declares one zone and initializes its local objects from the persistent session.
    /// </summary>
    public sealed class ZoneContext : MonoBehaviour
    {
        [SerializeField] private string zoneId;
        [SerializeField] private string objectiveId;
        [SerializeField, TextArea] private string objectiveText;

        private SpawnPoint[] spawnPoints;
        private readonly List<string> spawnIds = new List<string>();

        public string ZoneId => zoneId;

        public bool Initialize(
            GameSessionController session,
            SceneFlowController sceneFlow,
            ObjectiveController objectiveController,
            out string error)
        {
            if (session == null || sceneFlow == null || string.IsNullOrWhiteSpace(zoneId))
            {
                error = $"ZoneContext '{name}' is missing its runtime or ZoneId.";
                return false;
            }

            CacheSpawns();
            if (spawnPoints.Length == 0)
            {
                error = $"Zone '{zoneId}' has no SpawnPoint components.";
                return false;
            }

            ZoneExitInteractable[] exits = GetComponentsInChildren<ZoneExitInteractable>(true);
            for (int index = 0; index < exits.Length; index++)
            {
                exits[index].Bind(sceneFlow);
            }

            GameFlagId visitedFlag = zoneId == "hall"
                ? GameFlagId.HallVisited
                : zoneId == "arca" ? GameFlagId.ArcaVisited : GameFlagId.None;

            if (session.State.SetFlag(visitedFlag))
            {
                Debug.Log($"Flag set: {visitedFlag}", this);
            }

            objectiveController?.SetCurrent(objectiveId, objectiveText);
            error = null;
            return true;
        }

        public bool TryGetSpawn(string spawnId, out SpawnPoint spawnPoint, out string error)
        {
            CacheSpawns();
            if (SpawnIdResolver.TryResolveIndex(spawnIds, spawnId, out int index, out error))
            {
                spawnPoint = spawnPoints[index];
                return true;
            }

            spawnPoint = null;
            error = $"Zone '{zoneId}': {error}";
            return false;
        }

        private void CacheSpawns()
        {
            spawnPoints = GetComponentsInChildren<SpawnPoint>(true);
            spawnIds.Clear();

            for (int index = 0; index < spawnPoints.Length; index++)
            {
                spawnIds.Add(spawnPoints[index].SpawnId);
            }
        }
    }
}
