using Asteroids.Common.Enums;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.Ship
{
    public interface IUpdateShipStatusResult : IResult
    {
        ShipStatus Status { get; }
    }
}