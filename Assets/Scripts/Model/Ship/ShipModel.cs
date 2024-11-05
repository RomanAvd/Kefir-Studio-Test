using Asteroids.Common;
using Asteroids.Common.Settings;
using UnityEngine;

namespace Asteroids.Model.Ship
{
    public interface IShipModel
    {
        Vector2 SpeedVector { get; }
        float RotationAngle { get; }
        Vector2 Position { get; }
        void UpdateShipMovement(float timeDelta, float rotationAxis, bool thrustEnabled);
    }

    public class ShipModel : IShipModel
    {
        public Vector2 SpeedVector { get; private set; }
        public float RotationAngle { get; private set; }
        public Vector2 Position { get; private set; }


        private readonly IShipSettings _shipSettings;

        public ShipModel(IShipSettings shipSettings)
        {
            _shipSettings = shipSettings;
        }

        public void UpdateShipMovement(float timeDelta, float rotationAxis, bool thrustEnabled)
        {
            RotationAngle = (RotationAngle + rotationAxis * timeDelta * _shipSettings.RotationSpeed) % 360;

            if (thrustEnabled)
            {

                var accelerationVector = new Vector2(0, _shipSettings.AccelerationRate * timeDelta).Rotate(RotationAngle);
                SpeedVector = Vector2.ClampMagnitude(SpeedVector + accelerationVector, _shipSettings.MaxSpeed);
            }
            else
            {
                SpeedVector = Vector2.Lerp(SpeedVector, Vector2.zero, _shipSettings.DecelerationRate * timeDelta);
            }

            Position += SpeedVector;
        }
    }
}