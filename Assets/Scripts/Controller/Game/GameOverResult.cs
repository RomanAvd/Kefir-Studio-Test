using Asteroids.Common.Enums;
using Asteroids.Common.Observer;
using Asteroids.Controller.Ship;

namespace Asteroids.Controller.Game
{
    public interface IGameOverResult : IStateResult, IUpdateShipStatusResult
    {

    }

    public class GameOverResult : StateResult, IGameOverResult
    {
        public ShipStatus Status { get; }

        public GameOverResult(ShipStatus status) : base(GameState.GameOver)
        {
            Status = status;
        }
    }
}