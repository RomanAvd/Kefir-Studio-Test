using System.Collections.Generic;
using Asteroids.Common.Observer;

namespace Asteroids.Controller.MovingObjects
{
    public interface IUpdateMovingObjectsResult : IResult
    {
        IReadOnlyList<MovingObjectData> MovingObjects { get; }
        IReadOnlyList<int> RemovedObjects { get; }
    }

    internal sealed class UpdateMovingObjectsResult : IUpdateMovingObjectsResult
    {
        public IReadOnlyList<MovingObjectData> MovingObjects { get; }
        public IReadOnlyList<int> RemovedObjects { get; }

        public UpdateMovingObjectsResult(IEnumerable<MovingObjectData> movingObjects, IEnumerable<int> removedObjects)
        {
            MovingObjects = new List<MovingObjectData>(movingObjects);
            RemovedObjects = new List<int>(removedObjects);
        }
    }
}