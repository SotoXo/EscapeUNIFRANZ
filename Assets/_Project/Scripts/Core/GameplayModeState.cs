using System;

namespace EscapeUNIFRANZ.Core
{
    /// <summary>
    /// Pure mode state used by the runtime controller and EditMode tests.
    /// </summary>
    public sealed class GameplayModeState
    {
        public GameplayMode Current { get; private set; } = GameplayMode.Explore;
        public bool AllowsGameplay => Current == GameplayMode.Explore;

        public event Action<GameplayMode> Changed;

        public bool SetMode(GameplayMode mode)
        {
            if (Current == mode)
            {
                return false;
            }

            Current = mode;
            Changed?.Invoke(Current);
            return true;
        }
    }
}
