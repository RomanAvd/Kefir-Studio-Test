using System.Collections.Generic;
using System.Linq;
using Asteriods.Model.Movement;
using Asteriods.Model.Score;
using Asteroids.Controller.Game;
using Asteroids.Controller.MovingObjects;
using Asteroids.Controller.Ship;
using Asteroids.Model.Ship;

namespace Asteroids.Controller.Common
{
    internal static class ResultFactory
    {
        internal static IUpdateShipResult GetUpdateShipResult(IShipModel shipModel, bool thrustEnabled)
        {
            return new UpdateShipResult(
                shipModel.RotationAngle,
                shipModel.Position,
                shipModel.Speed,
                shipModel.Status,
                shipModel.SecondaryWeapon.Charges,
                shipModel.SecondaryWeapon.ChargeCooldown,
                shipModel.SecondaryWeapon.CooldownRemaining,
                thrustEnabled);
        }

        internal static IObjectDestroyedResult GetObjectDestroyedResult(int id, IScoreModel scoreModel)
        {
            return new ObjectDestroyedResult(id, scoreModel.Score);
        }

        internal static IGameOverResult GetGameOverResult(IShipModel shipModel)
        {
            return new GameOverResult(shipModel.Status);
        }

        internal static IStartGameResult GetStartGameResult(IScoreModel scoreModel)
        {
            return new StartGameResult(scoreModel.Score);
        }

        internal static IUpdateMovingObjectsResult GetUpdateMovingObjectsResult(IMovingObjectsModel movingObjectsModel, IEnumerable<int> objectsToRemove)
        {
            var movingObjectsData = movingObjectsModel.MovingObjects
                                                      .Select(m => new MovingObjectData(m.MovementModel.Position, m.MovementModel.Rotation, m.Id, m.ResourceKey));
            return new UpdateMovingObjectsResult(movingObjectsData, objectsToRemove);
        }
    }
}