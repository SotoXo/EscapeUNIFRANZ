using EscapeUNIFRANZ.Core;
using NUnit.Framework;

namespace EscapeUNIFRANZ.Tests.EditMode
{
    public sealed class GameStateTests
    {
        [Test]
        public void NewState_StartsWithoutFlags()
        {
            var state = new GameState();

            Assert.That(state.CompletedFlags, Is.Empty);
            Assert.That(state.HasFlag(GameFlagId.HallVisited), Is.False);
        }

        [Test]
        public void SetFlag_AddsFlag()
        {
            var state = new GameState();

            Assert.That(state.SetFlag(GameFlagId.HallVisited), Is.True);
            Assert.That(state.HasFlag(GameFlagId.HallVisited), Is.True);
        }

        [Test]
        public void SetFlag_IsIdempotent()
        {
            var state = new GameState();

            state.SetFlag(GameFlagId.HallVisited);

            Assert.That(state.SetFlag(GameFlagId.HallVisited), Is.False);
            Assert.That(state.CompletedFlags.Count, Is.EqualTo(1));
        }

        [Test]
        public void HasFlag_ReturnsCorrectValue()
        {
            var state = new GameState();
            state.SetFlag(GameFlagId.ArcaVisited);

            Assert.That(state.HasFlag(GameFlagId.ArcaVisited), Is.True);
            Assert.That(state.HasFlag(GameFlagId.HallVisited), Is.False);
            Assert.That(state.HasFlag(GameFlagId.None), Is.False);
        }

        [Test]
        public void ChangingZone_DoesNotRemoveFlags()
        {
            var state = new GameState();
            state.SetFlag(GameFlagId.HallVisited);

            state.SetLocation("arca", "from_hall");

            Assert.That(state.HasFlag(GameFlagId.HallVisited), Is.True);
            Assert.That(state.CurrentZoneId, Is.EqualTo("arca"));
        }
    }
}
