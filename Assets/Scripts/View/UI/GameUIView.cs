using Asteroids.Common.Enums;

namespace Asteroids.View.View.UI
{
    internal sealed class GameUIView : UIStateView
    {
        protected override GameState State => GameState.Game;
    }
}