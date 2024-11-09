using Asteroids.Controller.Ship;
using Asteroids.Model.Ship;

namespace Asteroids.Controller.Common
{
    internal static class ResultFactory
    {
        internal static IUpdateShipResult GetUpdateShipResult(IShipModel shipModel)
        {
            return new UpdateShipResult(
                shipModel.RotationAngle,
                shipModel.Position,
                shipModel.Speed,
                shipModel.Status,
                shipModel.SecondaryWeapon.Charges,
                shipModel.SecondaryWeapon.ChargeCooldown,
                shipModel.SecondaryWeapon.CooldownRemaining);
        }
    }
}