namespace Asteriods.Model.Movement
{
    public interface IMovingObject
    {
        int Id { get; }
        string ResourceKey { get; }
        IMovementModel MovementModel { get; }
    }

    internal sealed class MovingObject : IMovingObject
    {
        public int Id { get; }
        public string ResourceKey => _resourceKey;
        public IMovementModel MovementModel => _movementModel;

        private readonly string _resourceKey;
        private readonly IMovementModel _movementModel;

        public MovingObject(string resourceKey, IMovementModel movementModel)
        {
            Id = GetHashCode();
            _resourceKey = resourceKey;
            _movementModel = movementModel;
        }
    }
}