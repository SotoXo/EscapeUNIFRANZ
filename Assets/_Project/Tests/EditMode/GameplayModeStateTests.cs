using EscapeUNIFRANZ.Core;
using NUnit.Framework;

namespace EscapeUNIFRANZ.Tests.EditMode
{
    public sealed class GameplayModeStateTests
    {
        [Test]
        public void NewState_StartsInExplore()
        {
            var state = new GameplayModeState();

            Assert.That(state.Current, Is.EqualTo(GameplayMode.Explore));
            Assert.That(state.AllowsGameplay, Is.True);
        }

        [Test]
        public void Transition_BlocksGameplay()
        {
            var state = new GameplayModeState();

            state.SetMode(GameplayMode.Transition);

            Assert.That(state.AllowsGameplay, Is.False);
        }

        [Test]
        public void ReturningToExplore_EnablesGameplay()
        {
            var state = new GameplayModeState();
            state.SetMode(GameplayMode.Transition);

            state.SetMode(GameplayMode.Explore);

            Assert.That(state.AllowsGameplay, Is.True);
        }
    }
}
