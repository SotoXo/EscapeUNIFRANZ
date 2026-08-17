using System;
using System.Collections.Generic;
using UnityEngine;

namespace EscapeUNIFRANZ.Core
{
    /// <summary>
    /// Serializable session state. It contains no scene objects or presentation logic.
    /// </summary>
    [Serializable]
    public sealed class GameState
    {
        [SerializeField] private string currentZoneId = string.Empty;
        [SerializeField] private string currentSpawnId = string.Empty;
        [SerializeField] private string currentObjectiveId = string.Empty;
        [SerializeField] private List<GameFlagId> completedFlags = new List<GameFlagId>();

        public string CurrentZoneId => currentZoneId;
        public string CurrentSpawnId => currentSpawnId;
        public string CurrentObjectiveId => currentObjectiveId;
        public IReadOnlyList<GameFlagId> CompletedFlags => completedFlags;

        public bool HasFlag(GameFlagId flag)
        {
            return flag != GameFlagId.None && completedFlags.Contains(flag);
        }

        public bool SetFlag(GameFlagId flag)
        {
            if (flag == GameFlagId.None || completedFlags.Contains(flag))
            {
                return false;
            }

            completedFlags.Add(flag);
            return true;
        }

        public void SetLocation(string zoneId, string spawnId)
        {
            currentZoneId = zoneId ?? string.Empty;
            currentSpawnId = spawnId ?? string.Empty;
        }

        public void SetCurrentObjective(string objectiveId)
        {
            currentObjectiveId = objectiveId ?? string.Empty;
        }
    }
}
